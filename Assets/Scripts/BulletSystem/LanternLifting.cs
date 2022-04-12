using System;
using System.Collections.Generic;
using Control;
using UnityEngine;
using BulletSystem;
using BulletImplementation;


///Lanterns prefab will need to be updated when the special bullets are available
public class LanternLifting : MonoBehaviour
{
    // Start is called before the first frame update
    float maxLift;
    float currentLift;
    float ySpeed;
    float y;
    float maxTime;
    float clockSpeed;
    void Start()
    {
        switch((int)transform.position.z){
            case 1:
                maxLift=7F;
                break;
            case 2:
                maxLift=6.85F;
                break;
            case 3:
                maxLift=6.6F;
                break;
            case 4:
                maxLift=6.65F;
                break;
            case 5:
                maxLift=4.5F;
                break;
            case 6:
                maxLift=4.35F;
                break;
            case 7:
                maxLift=4.1F;
                break;
            case 8:
                maxLift=4.15F;
                break;
            case 9:
                maxLift=2F;
                break;
            case 10:
                maxLift=1.85F;
                break;
            case 11:
                maxLift=1.6F;
                break;
            case 12:
                maxLift=1.65F;
                break;
            default:
                break;
        }
        clockSpeed=0.1F;
        maxTime=60;
        //Debug.Log(clockSpeed);
        //Debug.Log(maxTime);
        currentLift=0;
        ySpeed=(float)0.05;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLift<maxLift){
            maxTime-=clockSpeed;
            currentLift+=ySpeed;
            y = (float)(transform.position.y+ySpeed);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        else{
            //Debug.Log(maxTime);
            if(maxTime<=1.5){
                GetComponent<BulletLauncher2>().enabled=true;
                GetComponent<BulletLauncher2>().rotationangle=UnityEngine.Random.Range(50F, 360F);
                GetComponent<BulletLauncher2>().bulletSpeed=(int)UnityEngine.Random.Range(1F, 3F);
            }
            if(maxTime<=0){
                foreach(Transform child in transform){
                    child.gameObject.SetActive(false);
                }
                GetComponent<BulletLauncher2>().enabled=false;
            }
            maxTime-=clockSpeed;
        }
    }
}
