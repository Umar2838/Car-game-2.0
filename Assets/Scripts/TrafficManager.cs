using UnityEngine;
using System.Collections;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[]  lane ;
    [SerializeField] GameObject[]  trafficVehicle;
    [SerializeField] CarControllers carController;
    [SerializeField] float minSpawnTime = 30f;
    [SerializeField] float maxSpawnTime = 60f;
    private float dynamicCounter = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }

    IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while(true){
           if(carController.CarSpeed() > 20f){
             dynamicCounter = Random.Range(minSpawnTime,maxSpawnTime)/carController.CarSpeed();
            SpawnVehicle();
                }
            yield return new WaitForSeconds(dynamicCounter);
        }

    }
    void SpawnVehicle(){
        int randomLaneIndex = Random.Range(0,lane.Length);
        int randomVehicleIndex = Random.Range(0,trafficVehicle.Length);
        Instantiate(trafficVehicle[randomVehicleIndex],lane[randomLaneIndex].position , Quaternion.identity);
    }
}
