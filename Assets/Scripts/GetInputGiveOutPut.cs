using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetInputGiveOutPut : MonoBehaviour
{
    public InputField Down_Payment;
    public InputField House_Price;
    public InputField Interest_Rate;
    public Dropdown Amortization_Period_Drop;
    public Dropdown Payment_Frequency_Drop;
    public Text Mortgage_Payment;
    public Text Number_of_Payments;
    public Text Interest_Payments;
    public Text Total_Cost;


    public void setget()
    {

    }

    

    
    
    public void MortgagePayment()
    {
        //M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]

        double amo;

        amo = Amortization_Period_Drop.value * 5 + 5;
        double n = amo * 12f;

        double m;
        double i = double.Parse(Interest_Rate.text)/100f/12f;
        double p = double.Parse(House_Price.text) - double.Parse(Down_Payment.text);
        double interest1 = Math.Pow(i + 1, n);



        m = (p*(interest1 * i))/(interest1 - 1);

        double pLeft; // principle left after payment
        double iPay; // amount of payment going towards interest
        double ttlInterest = 0;
        double payments = 0;
        double paymentYears = 0;

        if (Payment_Frequency_Drop.value == 0)
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
        else if (Payment_Frequency_Drop.value == 1)
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
        else if (Payment_Frequency_Drop.value == 2)
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
        double ttlcost = p + ttlInterest;
        Mortgage_Payment.text = m.ToString("F2");
        Number_of_Payments.text = payments.ToString() + " (" + paymentYears.ToString("F2") + " Years)";
        Interest_Payments.text = ttlInterest.ToString("F2");
        Total_Cost.text = ttlcost.ToString("F2");



    }
    
}
