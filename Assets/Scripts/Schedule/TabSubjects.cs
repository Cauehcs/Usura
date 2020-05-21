using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TabSubjects : MonoBehaviour{

    [SerializeField] GameObject[] groupAddingNewSubject;
    public GameObject inputFieldSubject, btnConfirmAddSubject, prefableText;
    public Transform content;

    public List<Vector2> ListPosition = new List<Vector2>();
    public List<GameObject> ListGameObject = new List<GameObject>();
    int[] posYPrefableText;    

    private void Awake() {

        SetPosition();

    }

   

    private void Update() {

        if(inputFieldSubject.GetComponent<InputField>().text == "") btnConfirmAddSubject.GetComponent<Button>().interactable = false;
            
        else btnConfirmAddSubject.GetComponent<Button>().interactable = true;

        if (ListPosition.Capacity == ListGameObject.Capacity) SetPosition();

    }

    public void BtnAddSubject() {

        SceneController.instace.lockedEscape = true;

        foreach (GameObject item in groupAddingNewSubject) {

            item.SetActive(true);

        }

    }

    public void BtnCancelAddSubject() {

        SceneController.instace.lockedEscape = false;

        inputFieldSubject.GetComponent<InputField>().text = "";

        foreach (GameObject item in groupAddingNewSubject) {

            item.SetActive(false);

        }

    }

    public void BtnConfirmAddSubject() {

        ListController();

        BtnCancelAddSubject();

    }

    public void ListController() {

        if(ListPosition.Capacity == ListGameObject.Capacity) AddPrefable();

    }

    public void SetPosition() {

        for (int i = 0; i < ListGameObject.Capacity; i++) {

                ListGameObject[i].GetComponent<RectTransform>().localPosition = ListPosition[i];

            }

    }

    public async void AddPrefable() {

        ListPosition.Insert(0, new Vector2(-20, ListPosition[0].y));

        ListGameObject.Insert(0, Instantiate(prefableText, ListPosition[0], Quaternion.Euler(Vector3.zero), content));

        for (int i = 1; i < ListGameObject.Capacity; i++) {
            
            ListPosition[i] = new Vector2(-20, (ListPosition[i].y - 45));

        }

        await Task.Delay(100);

        SetPosition();

    }

}
