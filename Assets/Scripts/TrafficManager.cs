using UnityEngine;
using System.Collections;
public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[]  lanes ;
    [SerializeField] GameObject[]  trafficVehicles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }

    IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while(true){
            Instantiate(trafficVehicles[0],lanes[0].position , Quaternion.identity);
                yield return new WaitForSeconds(2f);
        }

    }
}
