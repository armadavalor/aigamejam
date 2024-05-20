using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class AjanMl : Agent
{
    [SerializeField] float multiplier = 2f;
    [SerializeField] private Transform hedef;
    private Rigidbody rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public override void Heuristic(in ActionBuffers actionsOut) //Kontrolü ele alabilmek için Heuristic kullanıyoruz.
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxis("Horizontal");
        continousActions[1] = Input.GetAxis("Vertical");
    }
    public override void OnEpisodeBegin()
    {

        transform.localPosition = new Vector3(0f, 0.3f, 0f);
      //  hedef.localPosition = new Vector3(Random.value * 8.5f - 4f, 0.5f, Random.value * 8.5f - 4f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(hedef.localPosition);
        sensor.AddObservation(hedef.transform.position - gameObject.transform.position);

        sensor.AddObservation(rbody.velocity.x);
        sensor.AddObservation(rbody.velocity.y);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 kontrol = Vector3.zero;
        kontrol.x = actions.ContinuousActions[0];
        kontrol.z = actions.ContinuousActions[1];
        rbody.AddForce(kontrol * multiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hedef")
        {
            AddReward(10f);
            EndEpisode();
        }

        if (other.gameObject.tag == "Duvar")
        {
            AddReward(-5f);
            EndEpisode();
        }
    }
}
