using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    void Start()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 5;   
    }

    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 5;         
    }
}
