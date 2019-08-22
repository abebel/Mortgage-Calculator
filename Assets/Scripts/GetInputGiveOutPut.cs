using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetInputGiveOutPut : MonoBehaviour
{
    /// <summary>
    /// need to add amo period over 25 years
    /// when over 25 years mortgage insurance isnt available so the 
    /// person needs to have 20% downpayment to get a mortgage
    /// 
    /// Also need to look at how mortgage insurance is calculated into
    /// the total cost. Appears as if the value isn't charged any interest
    /// over the life of the loan.
    /// 
    /// Also add weekly and accelerated weekly options for payment
    /// frequency
    /// </summary>
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


    public void setget()
    {

    }

    

    
    
    public void MortgagePayment()
    {
        //M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]
        //Down Payment (% of Home Price)
        //5 % -9.99 % 10 % -14.99 % 15 % -19.99 %
        //4.00%	         3.10%	       2.80%	

        double dPayment = double.Parse(DownPayment.text);
        double hPrice = double.Parse(HousePrice.text);
        double iRate = double.Parse(InterestRate.text);
        double minDPayment = hPrice * 0.05f;

        double amo;

        amo = AmortizationPeriodDrop.value * 5 + 5;
        double n = amo * 12f;

        double m;
        double i = iRate/100f/12f;
        double p = hPrice - dPayment;
        double interest1 = Math.Pow(i + 1, n);
        double insuranceCost = 0;

        double ratio = dPayment / hPrice * 1f;
        if (ratio < .2)
        {
            MortgageInsurance.gameObject.SetActive(true);
            MortgageInsuranceLabel.gameObject.SetActive(true);
            if (ratio < .05)
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

        //if downpayment is less than 5 % then give a warning and assume 5 %
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




        m = (p*(interest1 * i))/(interest1 - 1);

        double pLeft; // principle left after payment
        double iPay; // amount of payment going towards interest
        double ttlInterest = 0;
        double payments = 0;
        double paymentYears = 0;

        if (PaymentFrequencyDrop.value == 0)
        {
            iPay = (p*i);
            ttlInterest += iPay;
            pLeft = p - m - iPay;
            payments++;
            while (pLeft > 0)
            {
                iPay = pLeft * i;
                pLeft -= (m - iPay);
                ttlInterest += iPay;
                payments++;
            }
            paymentYears = payments / 12f;

        }
        else if (PaymentFrequencyDrop.value == 1)
        {
            m = m * 12 / 26;
            iPay = p*(i*12/26f);
            ttlInterest += iPay;
            pLeft = p - m - iPay;
            payments++;
            while (pLeft > 0)
            {
                iPay = pLeft * (i*12/26f);
                pLeft -= (m - iPay);
                ttlInterest += iPay;
                payments++;
            }
            paymentYears = payments / 26f;
        }
        else if (PaymentFrequencyDrop.value == 2)
        {
            m /= 2;
            iPay = p * (i * 12 / 26f);
            ttlInterest += iPay;
            pLeft = p - (m - iPay);
            payments++;
            while (pLeft > 0)
            {
                iPay = pLeft * (i*12/26f);
                pLeft -= (m - iPay);
                ttlInterest += iPay;
                payments++;
            }
            paymentYears = payments / 26f;
        }
        double ttlcost = p + ttlInterest + insuranceCost;
        Mortgage_Payment.text = "$" + m.ToString("F2");
        NumberOfPayments.text = "$" + payments.ToString() + " (" + paymentYears.ToString("F2") + " Years)";
        InterestPayments.text = "$" + ttlInterest.ToString("F2");
        TotalCost.text = "$" + ttlcost.ToString("F2");



    }
    
}
