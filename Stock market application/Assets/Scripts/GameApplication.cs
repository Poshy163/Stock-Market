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
    private const float _yNegbounds = -2.9f;
    private const float _yPosbounds = 2.9f;

    [Header("Values")] public double money;

    public double tickTimer = 2;

    [Header("Objects")] public GameObject line;

    public TMP_Text stock_price;
    public TMP_Text yourMoney;

    private readonly float[] _presetXpos =
    {
        -7.7f,

        -5.775f,

        -3.85f,

        -1.925f,

        0,

        1.925f,

        3.85f,

        5.775f,

        7.7f
    };

    private LineRenderer _lineRenderer;


    [Header("Other")] private float[] _stockPrice =
    {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0
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
        0f
    };

    public void Start()
    {
        _lineRenderer = line.GetComponent<LineRenderer>();
        Startgame();
    }

    private void Update()
    {
        yourMoney.text = "Money: $" + money;
        _timer += Time.deltaTime;
        if (!(_timer >= tickTimer)) return;
        _timer = 0;
        Tick();
    }


    public void Buy()
    {
    }

    public void Sell()
    {
    }

    private void Startgame()
    {
        RenderLine();
    }

    private void RenderLine()
    {
        var count = 0;
        _lineRenderer.positionCount = _stockPrice.Length;
        foreach (var i in _stockPrice)
        {
            _lineRenderer.SetPosition(count, new Vector3(_presetXpos[count], i, 0));
            count++;
        }
    }

    private void Tick()
    {
        for (var i = 0; i <= _stockPrice.Length - 2; i++)
            tempArray[i] = _stockPrice[i + 1];
        tempArray[_stockPrice.Length - 1] = (float) Math.Round(Random.Range(_yPosbounds, _yNegbounds), 2);
        _stockPrice = tempArray;
        stock_price.text = "Price: $" + _stockPrice[_stockPrice.Length - 1] * 100;
        RenderLine();
    }
}