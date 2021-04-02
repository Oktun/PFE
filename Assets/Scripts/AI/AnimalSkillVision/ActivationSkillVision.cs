using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationSkillVision : MonoBehaviour
{
    public  bool IsActivated = false;
    public AnimalSkillVision[] animalsVisionObjects;

    private void Start()
    {
        animalsVisionObjects = FindObjectsOfType<AnimalSkillVision>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
            Activation(10);
        else if (Input.GetKey(KeyCode.N))
            Activation(20);

    }

    public void Activation(int time)
    {
        if (IsActivated)
        {

            if(time>6 && time< 18)
            {
                foreach(AnimalSkillVision animal in animalsVisionObjects)
                {
                    animal.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (AnimalSkillVision animal in animalsVisionObjects)
                {
                    animal.gameObject.SetActive(true);
                }
            }
        }
    }
}
