using System;
using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Sensors.Reflection;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HayvanatDeneme : Agent
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] private Transform ot;
    private Rigidbody rbody;
    private GameObject kup;



    public override void Initialize()
    {
        rbody = GetComponent<Rigidbody>();

    }



    public override void Heuristic(in ActionBuffers actionsOut) //Kontrolü ele alabilmek için Heuristic kullanıyoruz.
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
    }
    public override void OnEpisodeBegin()
    {

       // transform.localPosition = new Vector3(0f, 0.3f, 0f);
        //ot.localPosition = new Vector3(Random.value * 18 - 6f, 0.35f, Random.value * 18 - 6f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
       // sensor.AddObservation(ot.localPosition);
       // sensor.AddObservation(ot.transform.position - gameObject.transform.position);

    }
  // [Observable(numStackedObservations: 9)]
  // Vector3 PositionDelta
  // {
  //     get
  //     {
  //         return ot.transform.position - gameObject.transform.position;
  //     }
  // }



    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveRotate = actions.ContinuousActions[0];
        float moveForward = actions.ContinuousActions[1];

        rbody.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
        transform.Rotate(0f,moveRotate * moveSpeed,0f,Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ot")
        {
            AddReward(5f);
            EndEpisode();
        }
        //Kurt dokunursa ölür.
        if (other.gameObject.tag == "Kurt")
        {
            AddReward(-100f);
            EndEpisode();
        }
        if (other.gameObject.tag == "Duvar")
        {
            AddReward(-2.5f);
            EndEpisode();
        }
    }
}
