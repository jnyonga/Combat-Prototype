using UnityEngine;
using UnityEngine.UI;

public class Adrenaline : MonoBehaviour
{
    [Header("References")]
    public Image adrenalineFillBar;
    public Image fightFillBar;
    public Image flightFillBar;

    [Header("Adrenaline Settings")]
    [SerializeField]
    public float maxAdrenaline = 60f;
    [SerializeField]
    public float currentAdrenaline = 60f;
    [SerializeField]
    public float adrenalineDrainRateMulti = 1f;
    [SerializeField]
    public float minAdrenaline = 1f;
    [SerializeField]
    public bool inCombat = false;

    [Header("Fight Bar Settings")]
    [SerializeField]
    public float maxFight = 100f;
    [SerializeField]
    public float currentFight = 0f;
    [SerializeField]
    public float fightActiveDuration = 30f;
    [SerializeField]
    public bool isFightActive = false;

    [Header("Flight Bar Settings")]
    [SerializeField]
    public float maxFlight = 100f;
    [SerializeField]
    public float currentFlight = 0f;
    [SerializeField]
    public float flightActiveDuration = 30f;
    [SerializeField]
    public bool isFlightActive = false;

    public void Start()
    {
        currentAdrenaline = maxAdrenaline;
    }
    public void Update()
    {
        AdrenalineDrain();
    }
    public void AdrenalineDrain()
    {
        if (inCombat == false && currentAdrenaline > minAdrenaline)
        {
            currentAdrenaline -= (Time.deltaTime * adrenalineDrainRateMulti);
        }

        FillBarUpdate(adrenalineFillBar, currentAdrenaline, maxAdrenaline);
    }

    public void FillBarUpdate(Image fillBar, float curVal, float maxVal)
    {
        fillBar.fillAmount = curVal / maxVal;
    }

    public void TakeDamage(int damage)
    {
        if(currentAdrenaline > 0)
        {
            currentAdrenaline -= damage;
        }
        else if (currentAdrenaline <= 0)
        {
            currentAdrenaline = 0;
        }
    }

    public void FightMechanic(int )
    {
        FillBarUpdate(fightFillBar, currentFight, maxFight);
    }

    public void FlightMechanic()
    {
        FillBarUpdate(flightFillBar, currentFlight, maxFlight);
    }
}
