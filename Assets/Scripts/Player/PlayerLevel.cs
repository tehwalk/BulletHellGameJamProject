using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    private static PlayerLevel instance;
    public static PlayerLevel Instance { get { return instance; } }
    GameManager gameManager;
    WeaponActivator weaponActivator;
    int playerLevel = 1;
    bool canBeHit = true;
    public int Level { get { return playerLevel; } }
    [Header("Basic Properties")]
    // [SerializeField] int xpPoints = 2;
    [SerializeField] int xpRequiredStarting = 10;
    [SerializeField] int xpIncrement = 10;
    int xpPointsSum = 0, xpRequired;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float enemyHitRate;
    [SerializeField] GameObject bloodGFX;
    float enemyHitTime = 0;
    [Header("GUI Properties")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider xpBar;

    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        weaponActivator = GetComponent<WeaponActivator>();
        xpRequired = xpRequiredStarting;
        xpPointsSum = xpRequired / 2;
        UpdateXPPointsGUI();
        UpdateLevelGUI();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /* if (other.CompareTag("XP"))
         {
             CollectXP();
             Destroy(other.gameObject);
         }*/
        switch (other.tag)
        {
            case Tags.T_XP:
                CollectXP(2);
                Destroy(other.gameObject);
                break;
            case Tags.T_XP2:
                CollectXP(4);
                Destroy(other.gameObject);
                break;
            case Tags.T_XP3:
                CollectXP(6);
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.T_Enemy) && canBeHit == true)
        {
            Debug.Log("melee hit");
            LoseXP(other.gameObject.GetComponent<EnemyBehaviour>().Damage);
            var blood = Instantiate(bloodGFX, transform.position, Quaternion.identity);
            Destroy(blood, 0.9f);
            StartCoroutine(Invulnerablity());
        }
    }

    IEnumerator Flash()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    IEnumerator Invulnerablity()
    {
        canBeHit = false;
        yield return new WaitForSeconds(enemyHitRate);
        canBeHit = true;
    }

    void CollectXP(int xpPoints)
    {
        xpPointsSum += xpPoints;
        UpdateXPPointsGUI();
        if (xpPointsSum >= xpRequired)
        {
            playerLevel += 1;
            weaponActivator.IncreaseLifeTime();
            IncreaseXPRequired();
            xpPointsSum = 0;
        }
    }


    public void LoseXP(int dmg)
    {
        StartCoroutine(Flash());
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
            weaponActivator.DecreaseLifeTime();
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
