using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    GameManager gameManager;
    int playerLevel = 1;
    [Header("Basic Properties")]
    [SerializeField] int xpPoints = 2;
    [SerializeField] int xpRequiredStarting = 10;
    [SerializeField] int xpIncrement = 10;
    int xpPointsSum = 0, xpRequired;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float enemyHitRate;
    float enemyHitTime = 0;
    [Header("GUI Properties")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider xpBar;

    private void Start()
    {
        gameManager = GameManager.Instance;
        xpRequired = xpRequiredStarting;
        xpPointsSum = xpRequired / 2;
        UpdateXPPointsGUI();
        UpdateLevelGUI();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("XP"))
        {
            CollectXP();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("melee hit");
            StartCoroutine(DamageOverTime(other.gameObject.GetComponent<EnemyBehaviour>().Damage));
        }
    }

    IEnumerator DamageOverTime(int dmg)
    {
       LoseXP(dmg);
       yield return new WaitForSeconds(enemyHitRate);
    }

    void CollectXP()
    {
        xpPointsSum += xpPoints;
        UpdateXPPointsGUI();
        if (xpPointsSum >= xpRequired)
        {
            playerLevel += 1;
            IncreaseXPRequired();
            xpPointsSum = 0;
        }
    }


    public void LoseXP(int dmg)
    {
        xpPointsSum -= dmg;
        UpdateXPPointsGUI();
        if (xpPointsSum <= 0)
        {
            playerLevel -= 1;
            if (playerLevel <= 0)
            {
                Debug.Log("DEd");
                gameManager.Lost();
            }
            DecreaseXPRequired();
        }
    }

    void IncreaseXPRequired()
    {
        xpRequired = xpRequired + xpIncrement;
        UpdateLevelGUI();
    }

    void DecreaseXPRequired()
    {
        xpRequired = xpRequired - xpIncrement;
        UpdateLevelGUI();
    }

    #region GUI Methods

    void UpdateXPPointsGUI()
    {
        xpBar.value = ExtentionMethods.MapValueToRange(xpPointsSum, 0, xpRequired, xpBar.minValue, xpBar.maxValue);
    }
    void UpdateLevelGUI()
    {
        levelText.text = playerLevel.ToString();
    }

    #endregion

}
