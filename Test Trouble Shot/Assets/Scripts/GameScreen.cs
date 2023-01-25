using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    // Для инкапсуляции мы можем даже сделать поля приватными и пометить атрибутом SerializeField, чтобы они были видны в инспекторе
    [SerializeField] private TextMeshProUGUI currentInMagazineLabel;
    [SerializeField] private TextMeshProUGUI totalAmmoLabel;
    [SerializeField] private TextMeshProUGUI intKills;
    private float scorePlayer;
    [SerializeField] private Slider hpBar;

    public void SetAmmo(int current, int total)
    {
        currentInMagazineLabel.text = current.ToString();
        totalAmmoLabel.text = total.ToString();
    }

    public void SetHealth(float health)
    {
        hpBar.value = health / 100;
    }


    public void GetDamage(float damage)
    {
        hpBar.value -= damage / 100;
    }

    public void AddKills(float score)
    {
        scorePlayer += score;
        intKills.text = scorePlayer.ToString();
    }
}
