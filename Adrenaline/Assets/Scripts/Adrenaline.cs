using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Adrenaline : MonoBehaviour
{
    [Header("References")]
    public Image adrenalineFillBar;
    public TextMeshProUGUI debugAdrenaline;
    public Image fightFillBar;
    public TextMeshProUGUI debugFight;
    public Image flightFillBar;
    public TextMeshProUGUI debugFlight;
    public Image fightBurnout;
    public Image flightBurnout;
    public TextMeshProUGUI debugStatus;

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

    [Header("Burnout Bar Settings")]
    [SerializeField]
    public float maxBurnout = 100f;
    [SerializeField]
    public float currentBurnout = 0f;
    [SerializeField]
    public bool isBurnoutActive = false;

    private float drainDuration = 30f;
    private float burnoutDuration = 15f;
    private float drainTimer = 0f;

    public void Start()
    {
        currentAdrenaline = maxAdrenaline;
    }
    public void Update()
    {
        AdrenalineDrain();

        FightMechanic();

        FlightMechanic();

        Burnout();

        DebugText();

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
    public void OnActivateFightPressed()
    {
        if(currentFight == maxFight && !isFlightActive && !isBurnoutActive)
        {
            isFightActive = true;
        }
    }
    public void FightMechanic()
    {
        FillBarUpdate(fightFillBar, currentFight, maxFight);

        if (isFightActive)
        {
            drainTimer += Time.deltaTime;
            currentFight = Mathf.Lerp(maxFight, 0f, drainTimer / drainDuration);

            if(drainTimer >= drainDuration)
            {
                currentFight = 0;
                isFightActive = false;
                drainTimer = 0;
                isBurnoutActive = true;
            }
        }
        
    }

    public void OnActivateFlightPressed()
    {
        if(currentFlight == maxFlight && !isFightActive && !isBurnoutActive)
        {
            isFlightActive = true;
        }
    }
    public void FlightMechanic()
    {
        FillBarUpdate(flightFillBar, currentFlight, maxFlight);

        if (isFlightActive)
        {
            drainTimer += Time.deltaTime;
            currentFlight = Mathf.Lerp(maxFlight, 0f, drainTimer / drainDuration);

            if (drainTimer >= drainDuration)
            {
                currentFlight = 0;
                isFlightActive = false;
                drainTimer = 0;
                isBurnoutActive = true;
            }
        }
    }
    public void Burnout()
    {
        FillBarUpdate(flightBurnout, currentBurnout, maxBurnout);
        FillBarUpdate(fightBurnout, currentBurnout, maxBurnout);

        if (isBurnoutActive)
        {
            drainTimer += Time.deltaTime;
            currentBurnout = Mathf.Lerp(maxBurnout, 0f, drainTimer / burnoutDuration);

            if (drainTimer >= drainDuration)
            {
                currentBurnout = 0;
                isBurnoutActive = false;
                drainTimer = 0;
            }
        }
        else
        {
            currentBurnout = 0;
        }
    }
    public void DebugText()
    {
        debugAdrenaline.text = $"{(currentAdrenaline/maxAdrenaline) * 100:F0}%";
        debugFight.text = $"{(currentFight/maxFight) * 100:F0}%";
        debugFlight.text = $"{(currentFlight/maxFlight) * 100:F0}%";

        if(isFightActive)
        {
            debugStatus.text = "Status: Fight Mode Active";
        }
        else if(isFlightActive)
        {
            debugStatus.text = "Status: Flight Mode Active";
        }
        else if(isBurnoutActive)
        {
            debugStatus.text = "Status: Burnout Mode Active";
        }
        else
        {
            debugStatus.text = "Status: Neutral";
        }
    }
}
