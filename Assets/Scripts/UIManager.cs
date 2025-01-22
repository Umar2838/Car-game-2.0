using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] CarControllers carController;
    [SerializeField] Transform carTransform;
    private float speed = 0f;
    private float distance = 0f;
    private float score = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUI();
        DistanceUI();
        ScoreUI();

    }
    void DistanceUI(){
        distance = carTransform.position.z / 1000;
        distanceText.text = distance.ToString("0.00"+"Km");
    }
    void SpeedUI(){
        speed = carController.CarSpeed();
        speedText.text = speed.ToString("0"+"Km/H");

    }
    void ScoreUI(){
        score = carTransform.position.z * 6;
        scoreText.text = score.ToString("0");
    }
}
