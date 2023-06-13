using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class Hp : MonoBehaviour
{

    Slider hpSlider;
    public int maxHp;
    void Start()
    {
        // スライダーを取得する
        hpSlider = GetComponent<Slider>();
    }

    void Update()
    {

    }
}