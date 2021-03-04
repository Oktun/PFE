using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : DefaultWeapon
{
    [System.Serializable]
    public class BowSetting
    {
        [Header("Arrow Setting")]
        public float arrowCount;
        public float fireRate = .8f;
        public Rigidbody arrowPrefab;
        public Transform arrowPos;
        public Transform arrowEquipParent;
        public float arrowForce;
        
        [Header("Bow String Settings")]
        public Transform bowString;
        public Transform stringInitialPos;
        public Transform stringHandPullPos;
        public Transform stringIntialParent;
    }
    [SerializeField]
    public BowSetting bowSetting;

    [Header("Crosshair Settings")]
    public GameObject crossHairPrefab;
    public GameObject currentCrossHair;

    bool canPullString = false;
    bool canFireArrow = false;

    Rigidbody currentArrow;

    public void PickArrow() => bowSetting.arrowPos.gameObject.SetActive(true);

    public void DisableArrow() => bowSetting.arrowPos.gameObject.SetActive(false);

    public void PullString()
    {
        bowSetting.bowString.transform.position = bowSetting.stringHandPullPos.position;
        bowSetting.bowString.transform.parent = bowSetting.stringHandPullPos;
    }

    public void ReleaseString()
    {
        bowSetting.bowString.transform.position = bowSetting.stringInitialPos.position;
        bowSetting.bowString.transform.parent = bowSetting.stringIntialParent;
    }

    public void ShowCrossHaire(Vector3 crossHairePos)
    {
        if (!currentCrossHair)
            currentCrossHair = Instantiate(crossHairPrefab) as GameObject;

        currentCrossHair.transform.position = crossHairePos;
        currentCrossHair.transform.LookAt(Camera.main.transform);
    }

    public void RemoveCrossHair()
    {
        if (currentCrossHair)
            Destroy(currentCrossHair);
    }

    public override void Fire(Vector3 hitPoint)
    {
        Vector3 dir = hitPoint - bowSetting.arrowPos.position;
        currentArrow = Instantiate(bowSetting.arrowPrefab, bowSetting.arrowPos.localPosition, bowSetting.arrowPos.rotation)
            as Rigidbody;

        currentArrow.transform.position = bowSetting.arrowPos.position;
        currentArrow.transform.rotation = bowSetting.arrowPos.rotation;
        currentArrow.gameObject.SetActive(true);
        currentArrow.AddForce(dir * bowSetting.arrowForce, ForceMode.Force);

    }

    IEnumerator ActivateGameObjectAfterTime(GameObject gameObject)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(true);

    }
}
