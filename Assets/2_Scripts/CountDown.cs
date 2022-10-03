using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text _countText;
    [SerializeField] Color[] _countColors;
    [SerializeField] LauncherObj _launcherObj;
    [SerializeField] WeaponControl _weaponControl;

    float _countTime = 4;

    void Update()
    {
        _countTime -= Time.deltaTime;
        if (_countTime >= 1)
        {
            _countText.color = _countColors[(int)_countTime-1];
            _countText.text = ((int)_countTime).ToString();
        }
        else
        {
            _launcherObj.IsStart();
            _countText.text = "시작";
        }
    }

    public void ResultScore()
    {
        _countText.text = _weaponControl._nowScore.ToString();
    }

}
