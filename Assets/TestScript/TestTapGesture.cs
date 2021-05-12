using System;
using TouchScript.Gestures;
using UnityEngine;

[RequireComponent(typeof(TapGesture))]
public class TestTapGesture : MonoBehaviour
{
    private TapGesture _tapGesture;

    // Start is called before the first frame update
    private void Start()
    {
        _tapGesture = transform.GetComponent<TapGesture>();
        _tapGesture.Tapped += HandleTapGesture;
    }

    private void HandleTapGesture(object sender, EventArgs e)
    {
        Debug.Log($"TAPPED {gameObject.name } in pos {_tapGesture.GetScreenPositionHitData().Point}");
    }

    // Update is called once per frame
}