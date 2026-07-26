using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{

    [SerializeField]
    private float depletionTime = 200;

    private float _secondsPerPercentage;
    private float _heightPerPercentage;

    private int _currentOxygen = 100;

    private Image _oxygenBar;

    private float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _oxygenBar = GetComponent<Image>();

        _secondsPerPercentage = depletionTime / 100f;
        _heightPerPercentage = _oxygenBar.rectTransform.rect.height / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _secondsPerPercentage)
        {
            _timer -= _secondsPerPercentage;
            _currentOxygen -= 1;
            Vector3 newPos = _oxygenBar.rectTransform.localPosition;
            newPos.y -= _heightPerPercentage / 2f;
            _oxygenBar.rectTransform.localPosition = newPos;

            Vector2 newSize = _oxygenBar.rectTransform.sizeDelta;
            newSize.y -= _heightPerPercentage;
            _oxygenBar.rectTransform.sizeDelta = newSize;

            if (_currentOxygen < 51 )
            {
                _oxygenBar.color = Color.yellow;
            }

            if (_currentOxygen < 15)
            {
                _oxygenBar.color = Color.red;
            }

            if (_currentOxygen <= 0)
            {
                //some sound effect
                //collapse animtion? 
                //
                SceneManager.LoadScene("GameOver");  
            }
        }
    }
}
