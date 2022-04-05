using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStatics : MonoBehaviour
{
    public string[] resourcesNames;
    public int[] resourcesQuanity;

    public int produce_at;
    public int roomLevel;
    public string[] planets;
    public string[] rooms;

    public string[] producedResources;
    public int[] producedResourcesQuantity;

    private GameObject mario;
    private int work = 0;

    void Start(){
        mario = GameObject.Find("marioIdle");
    }

    public bool CheckRequirements(){
        mario = GameObject.Find("marioIdle");
        int SatisfiedRequirements = 0;
        for(int i=0; i<resourcesNames.Length; i++){
            if(mario.GetComponent<ResourcesClass>().resources[resourcesNames[i]] >= resourcesQuanity[i]) SatisfiedRequirements++;
        }
        if(SatisfiedRequirements < resourcesNames.Length) return false;

        SatisfiedRequirements = 0;
        foreach(string i in mario.GetComponent<BuildRegulator>().GetBuiltRooms()){
            foreach(string j in rooms){
                if(i==j) SatisfiedRequirements++;
            }
        }
        if(SatisfiedRequirements < rooms.Length) return false;

        SatisfiedRequirements = 0;
        foreach(string i in mario.GetComponent<BuildRegulator>().unlockedPlanets){
            foreach(string j in planets){
                if(i==j) SatisfiedRequirements++;
            }
        }
        if(SatisfiedRequirements < planets.Length) return false;

        return true;
    }

    public void Produce(){
        work++;
        Debug.Log(gameObject.name);
        if(work>=produce_at * (1/roomLevel)){
            if(gameObject.name == "Lab(Clone)"){
                GameObject room = gameObject.GetComponent<LabProduce>().UnlockRoom();
                if(room!=null){
                    GameObject.Find("marioIdle").gameObject.GetComponent<BuildRegulator>().unlockedRooms.Add(room);
                }
            }else if(gameObject.name == "Communications(Clone)"){
                gameObject.GetComponent<CommProduce>().RecieveRandomResources();
            }else{
                for(int i=0; i<producedResources.Length; i++){
                    GameObject.Find("marioIdle").gameObject.GetComponent<ResourcesClass>().AddResource(producedResources[i], producedResourcesQuantity[i]);
                }
            }
            work=0;
        }
    }
}
