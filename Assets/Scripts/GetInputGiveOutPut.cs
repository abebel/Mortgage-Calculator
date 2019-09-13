using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetInputGiveOutPut : MonoBehaviour
{
     //need to add amo period over 25 years
     //when over 25 years mortgage insurance isnt available so the 
     //person needs to have 20% downpayment to get a mortgage
     
     //Also need to look at how mortgage insurance is calculated into
     //the total cost.Appears as if the value isn't charged any interest
     //over the life of the loan.
     
     //Also add weekly and accelerated weekly options for payment
     //frequency


    public InputField DownPayment;
    public InputField HousePrice;
    public InputField InterestRate;
    public Dropdown AmortizationPeriodDrop;
    public Dropdown PaymentFrequencyDrop;
    public Text Mortgage_Payment;
    public Text NumberOfPayments;
    public Text InterestPayments;
    public Text TotalCost;
    public Text DownPaymentWarning;
    public Text MortgageInsurance;
    public Text MortgageInsuranceLabel;


    private void Calc(double monthlyMortgage, double Principle, double InterestRate, out double TotalInterestPaid, out double PaymentsMade)
    {
        TotalInterestPaid = 0;
        PaymentsMade = 0;
        double InterestPaid = 0;
        while (Principle > 0)
        {
            InterestPaid = Principle * InterestRate;
            TotalInterestPaid += InterestPaid;
            Principle = Principle - (monthlyMortgage - InterestPaid);
            PaymentsMade++;
        }
    }
    public void MortgageInsuranceFields()
    {
        double dPayment = double.Parse(DownPayment.text);
        double hPrice = double.Parse(HousePrice.text); 
        if (dPayment / hPrice < 0.2)
        {
            MortgageInsurance.gameObject.SetActive(true);
            MortgageInsuranceLabel.gameObject.SetActive(true);
            if (dPayment / hPrice < .05)
            {
                DownPaymentWarning.gameObject.SetActive(true);
            }
            else
            {
                DownPaymentWarning.gameObject.SetActive(false);
            }
        }
        else
        {
            MortgageInsurance.gameObject.SetActive(false);
            MortgageInsuranceLabel.gameObject.SetActive(false);
        }
    }
    public double MortgageIns(double p)
    {
        // move everything about insurance to here
        double dPayment = double.Parse(DownPayment.text);
        double hPrice = double.Parse(HousePrice.text);       
        double ratio = dPayment / hPrice;
        double minDPayment = hPrice * 0.05f;
        double insuranceCost = 0;

        if (ratio < .05)
        {
            DownPaymentWarning.text = $"Minimum downpayment for the your house is ${minDPayment.ToString("F2")} (5% of house price), " +
                $"this value has been used for the calculations.";
            p = hPrice - minDPayment;
            insuranceCost = p * 0.04f;
            MortgageInsurance.text = insuranceCost.ToString("F2");
        }
        else if (ratio >= 0.05 && ratio <= 0.0999)
        {
            insuranceCost = p * 0.04f;
            p += insuranceCost;
            MortgageInsurance.text = insuranceCost.ToString("F2");
        }
        else if (ratio >= 0.10 && ratio <= 0.15)
        {
            insuranceCost = p * 0.031f;
            p += insuranceCost;
            MortgageInsurance.text = insuranceCost.ToString("F2");
        }
        else if (ratio >= 0.15 && ratio < 0.2)
        {
            insuranceCost = p * 0.028f;
            p += insuranceCost;
            MortgageInsurance.text = insuranceCost.ToString("F2");
        }
        return p;
    }

    public void LoanInterest()
    {
        // move everything about interest to here
    }
    public void MortgagePayment()
    {
        //
        // limit only to mortgage payments
        //
        double dPayment = double.Parse(DownPayment.text);
        double hPrice = double.Parse(HousePrice.text);
        double iRate = double.Parse(InterestRate.text);
        double minDPayment = hPrice * 0.05f;

        double Amortization;

        Amortization = AmortizationPeriodDrop.value * 5 + 5;
        double n = Amortization * 12f;

        double fPayment;
        double i = iRate / 100f / 12f;
        double p = hPrice - dPayment;
        double interest1 = Math.Pow(i + 1, n);

        p = MortgageIns(p);

        fPayment = (p * (interest1 * i)) / (interest1 - 1);

        double TotalInterestPaid = 0;
        double NumberOfPaymentsMade = 0;
        double YearsOfPayments = 0;
        double BiWeekly = 12 / 26f;

        // Payment Frequency
        if (PaymentFrequencyDrop.value == 0)
        {
            Calc(fPayment, p, i, out TotalInterestPaid, out NumberOfPaymentsMade);
            YearsOfPayments = NumberOfPaymentsMade / 12f;
        }
        else if (PaymentFrequencyDrop.value == 1)
        {
            fPayment = fPayment * BiWeekly;
            i = i * BiWeekly;
            Calc(fPayment, p, i, out TotalInterestPaid, out NumberOfPaymentsMade);
            YearsOfPayments = NumberOfPaymentsMade / 26f;
        }
        else if (PaymentFrequencyDrop.value == 2)
        {
            fPayment = fPayment / 2;
            i = i * BiWeekly;
            Calc(fPayment, p, i, out TotalInterestPaid, out NumberOfPaymentsMade);
            YearsOfPayments = NumberOfPaymentsMade / 26f;
        }


        double ttlcost = p + TotalInterestPaid;
        Mortgage_Payment.text = "$" + fPayment.ToString("F2");
        NumberOfPayments.text = NumberOfPaymentsMade.ToString() + " (" + YearsOfPayments.ToString("F2") + " Years)";
        InterestPayments.text = "$" + TotalInterestPaid.ToString("F2");
        TotalCost.text = "$" + ttlcost.ToString("F2");
    }

}
