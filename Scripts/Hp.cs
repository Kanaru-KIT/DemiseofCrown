using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ���������Y�ꂸ�ɓ����

public class Hp : MonoBehaviour
{

    Slider hpSlider;
    public int maxHp;
    void Start()
    {
        // �X���C�_�[���擾����
        hpSlider = GetComponent<Slider>();
    }

    void Update()
    {

    }
}