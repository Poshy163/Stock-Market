using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable InconsistentNaming
#pragma warning disable CS0414


// ReSharper disable IdentifierTypo

public class GameApplication : MonoBehaviour
{
    private const float _xNegbounds = -7.8f;
    private const float _xPosbounds = 7.8f;
    private const float _yNegbounds = -3.14f;
    private const float _yPosbounds = 2.9f;
    
    [Header("Values")] 
    public double money = 100000;
    private int day = 1;
    public double Stockvalue;
    public int stockAmount;
    public double tickTimer = 2;
    public int multiplyer = 10;
    [Header("Objects")] 
    public GameObject line;
    public TMP_Text Change_Price;
    public TMP_Text Day;
    public TMP_Text Stock_Value;
    public TMP_Text yourMoney;
    public TMP_Text debugText;
    public TMP_Text shares_Owned;
    public GameObject GreenLine;
    public GameObject RedLine;
    public GameObject ColourLineParent;

    private readonly float[] _presetXpos =
    {
        -7.7f,
        
        -6.7375f,
        
        -5.775f,
        
        -4.8125f,

        -3.85f,
        
        -2.8875f,

        -1.925f,
        
        -0.9625f,

        0,
        
        0.9625f,

        1.925f,
        
        2.8875f,

        3.85f,
        
        4.8125f,

        5.775f,
        
        6.7375f,

        7.7f
    };

    private LineRenderer _lineRenderer;


    [Header("Other")] private float[] _stockPrice =
    {
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f
    };

    private float _timer;

    private readonly float[] tempArray =
    {
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f
    };

    public void Start()
    {
        _lineRenderer = line.GetComponent<LineRenderer>();
        Startgame();
    }

    private void Update()
    {
        yourMoney.text = "Money: $" + Math.Round(money);
        
        _timer += Time.deltaTime;
        if (!(_timer >= tickTimer)) return;
        _timer = 0;
        Tick();
    }


    public void Buy()
    {
        switch (money <= Stockvalue)
        {
            case true when stockAmount == 0:
                Application.Quit();
                break;
            case true:
                debugText.text = "No Money";
                break;
            default:
                money -= Stockvalue;
                stockAmount++;
                break;
        }

        shares_Owned.text = "Shares: " + stockAmount;
    }

    public void Sell()
    {
        if (stockAmount <= 0) return;
        stockAmount--;
        money += Stockvalue;
        shares_Owned.text = "Shares: " + stockAmount;
    }

    private void Startgame()
    {
        RenderLine();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void RenderLine()
    {
        var taggedObjects = GameObject.FindGameObjectsWithTag("Line");   
        foreach (var d in taggedObjects) {
            Destroy(d);
        }
        
        _lineRenderer.positionCount = _stockPrice.Length;
        
        for (var i = 0; i <  _stockPrice.Length; i++)
        {
            _lineRenderer.SetPosition(i, new Vector3(_presetXpos[i], _stockPrice[i], 0));


            try
            {

                if (_stockPrice[i] > _stockPrice[i + 1])
                {
                    var tempLine = Instantiate(RedLine,ColourLineParent.transform).GetComponent<LineRenderer>();
                    tempLine.positionCount = 2;
                    tempLine.SetPosition(0, new Vector3(_presetXpos[i], _stockPrice[i], 0));
                    tempLine.SetPosition(1, new Vector3(_presetXpos[i + 1], _stockPrice[i + 1], 0));
                }
                else if (_stockPrice[i] < _stockPrice[i + 1])
                {
                    var tempLine = Instantiate(GreenLine,ColourLineParent.transform).GetComponent<LineRenderer>();
                    tempLine.positionCount = 2;
                    tempLine.SetPosition(0, new Vector3(_presetXpos[i], _stockPrice[i], 0));
                    tempLine.SetPosition(1, new Vector3(_presetXpos[i + 1], _stockPrice[i + 1], 0));
                }

            }
            catch
            {
                // ignored
            }

        }
    }

    private void Tick()
    {
        day++;
        Day.text = "Day "+ day + "/80";
        
        if(day == 80)
            Application.Quit();
        
        for (var i = 0; i <= _stockPrice.Length - 2; i++)
            tempArray[i] = _stockPrice[i + 1];
        var stockChange = (float) Math.Round(Random.Range(_yPosbounds, _yNegbounds), 2);
        
        tempArray[_stockPrice.Length - 1] = stockChange;
        Stockvalue += Math.Round(stockChange, 2);
        _stockPrice = tempArray;
        Stock_Value.text = "Value: $" + Stockvalue * multiplyer;
        Change_Price.text = "Change: $" + _stockPrice[_stockPrice.Length - 1] * multiplyer;
        
        if(Stockvalue <= 0)
            Application.Quit();
        
        
        RenderLine();
    }
}