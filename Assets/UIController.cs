using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    const string distanceTextUnit = "m";
    PlayerScript player;
    Text distanceText;

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " " + distanceTextUnit;
    }
}
