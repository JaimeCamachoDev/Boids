using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlockingCmp : MonoBehaviour
{
    [SerializeField] VisualEffect VisualEffect;
    [SerializeField] Flocking Flocking;

    Vector2 prevSpeed;

    public float waitTimeToActivate = 2.5f;
    public float maxSpeed = 50f;
    public bool directorControlVisuals = false;

    void Start()
    {
        if (!VisualEffect) VisualEffect = GetComponent<VisualEffect>();
        if (!Flocking) Flocking         = GetComponent<Flocking>();
        StartCoroutine(PrepareFlockingInPosition());
    }

    private void OnDisable()
    {
        if (name == "Flocking_RattailFish_Circles")
        {
            Debug.Log("Flocking ratail DISABLE");
        }
    }

    IEnumerator PrepareFlockingInPosition()
    {
        /// Deactivate Visuals and set high velocity
        if(!directorControlVisuals) VisualEffect.enabled = false;
        prevSpeed = Flocking.speedRange;
        Flocking.speedRange = new Vector2 (1, maxSpeed);

        yield return new WaitForSeconds(waitTimeToActivate);

        /// Activate Visuals and normal velocity
        if (!directorControlVisuals) VisualEffect.enabled = true;
        Flocking.speedRange = prevSpeed;

    }

}
