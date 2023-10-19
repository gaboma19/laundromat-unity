using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Workshift : MonoBehaviour
{
    public static Workshift instance;
    private List<Laundry> activeLaundry = new List<Laundry>();
    private Laundry selectedLaundry;
    [SerializeField] private Timer timer;
    public float customerTimeInterval;
    [SerializeField] private GameObject customer;
    public static event Action OnLaundrySpawned;
    [SerializeField] private GameObject spawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        RegisterController.OnWorkshiftStart += StartWorkShift;
        Timer.OnTimerEnded += EndWorkShift;
        Order.OnOrderPlaced += AddActiveLaundry;
    }

    void StartWorkShift()
    {
        timer.timerIsRunning = true;
        StartCoroutine(SpawnCustomer());
    }

    void EndWorkShift()
    {
        StopCoroutine(SpawnCustomer());

        // show a "shift ended" UI element
    }

    IEnumerator SpawnCustomer()
    {
        while (true)
        {
            Instantiate(customer, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(customerTimeInterval);
        }
    }

    void AddActiveLaundry()
    {
        Laundry newLaundry = new Laundry();

        if (selectedLaundry is null)
        {
            newLaundry.isSelected = true;
            selectedLaundry = newLaundry;
        }
        activeLaundry.Add(newLaundry);
        OnLaundrySpawned.Invoke();
    }

    public List<Laundry> GetActiveLaundryList()
    {
        return activeLaundry;
    }

    // keeps track of points / score / currency

    // play “getting it done” - game loop.

}
