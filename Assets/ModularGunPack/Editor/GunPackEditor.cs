using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using Random = UnityEngine.Random;

[CustomEditor(typeof(GunPackScript))]
public class GunPackEditor : Editor
{
    private GunPackScript.frame_type_list oldFrameType;
    private GunPackScript.trigger_type_list oldTriggerType;
    private GunPackScript.barrel_type_list oldBarrelType;
    private GunPackScript.scope_type_list oldScopeType;
    private GunPackScript.stock_type_list oldStockType;
    private GunPackScript.magazine_type_list oldMagazineType;
    private GunPackScript.lod_type_list oldLodType;

    private GunPackScript myTarget;

    //GAMEOBJECT DEFENITIONS
    GameObject newFrameType;
    GameObject newTriggerType;
    GameObject newBarrelType;
    GameObject newScopeType;
    GameObject newStockType;
    GameObject newMagazineType;

    //GAMEOBJECT LIST DEFENITIONS
    List<List<GameObject>> frameGO = new List<List<GameObject>>();
    List<List<GameObject>> triggerGO = new List<List<GameObject>>();
    List<List<GameObject>> barrelGO = new List<List<GameObject>>();
    List<List<GameObject>> scopeGO = new List<List<GameObject>>();
    List<List<GameObject>> stockGO = new List<List<GameObject>>();
    List<List<GameObject>> magazineGO = new List<List<GameObject>>();

    private static GameObject DynamicGun = null;

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Customization");

        //RANDOMIZE PREVIEWS
        if (GUILayout.Button("Randomize"))
        {
            Debug.Log("Randomize filter values");

            myTarget.frameType = (GunPackScript.frame_type_list)((int)Random.Range(0, 3));
            myTarget.triggerType = (GunPackScript.trigger_type_list)((int)Random.Range(0, (int)GunPackScript.trigger_type_list.none + 1));
            myTarget.barrelType = (GunPackScript.barrel_type_list)((int)Random.Range(0, 3));
            myTarget.scopeType = (GunPackScript.scope_type_list)((int)Random.Range(0, (int)GunPackScript.scope_type_list.none + 1));
            myTarget.stockType = (GunPackScript.stock_type_list)((int)Random.Range(0, (int)GunPackScript.stock_type_list.none + 1));
            myTarget.magazineType = (GunPackScript.magazine_type_list)((int)Random.Range(0, (int)GunPackScript.magazine_type_list.none + 1));

            PreviewGun();
        }

        //REFERENCE TO SCRIPT ENUMS
        myTarget.frameType = (GunPackScript.frame_type_list)
            EditorGUILayout.EnumPopup("Frame Type", myTarget.frameType);

        myTarget.triggerType = (GunPackScript.trigger_type_list)
            EditorGUILayout.EnumPopup("Trigger Type", myTarget.triggerType);

        myTarget.barrelType = (GunPackScript.barrel_type_list)
            EditorGUILayout.EnumPopup("Barrel Type", myTarget.barrelType);

        myTarget.scopeType = (GunPackScript.scope_type_list)
            EditorGUILayout.EnumPopup("Scope Type", myTarget.scopeType);

        myTarget.stockType = (GunPackScript.stock_type_list)
            EditorGUILayout.EnumPopup("Stock Type", myTarget.stockType);

        myTarget.magazineType = (GunPackScript.magazine_type_list)
            EditorGUILayout.EnumPopup("Magazine Type", myTarget.magazineType);

        myTarget.lodType = (GunPackScript.lod_type_list)
            EditorGUILayout.EnumPopup("LOD Type", myTarget.lodType);

        //GUN NAMING
        myTarget.gunName = EditorGUILayout.TextField("Gun Name", myTarget.gunName);

        //TURN GENERATED PREVIEW INTO PREFAB
        if (GUILayout.Button("Generate gun into new Prefab"))
        {
            Debug.Log("Added current selection as children to new parent");

            //List<GameObject> newPrefab = new List<GameObject>();

            GameObject newGun = new GameObject(myTarget.gunName);
            //newGun.LocalLocation = Gun.LocalLocation;

            //int iLOD = (int)myTarget.lodType - 1;

            foreach (List<GameObject> frameList in frameGO)
            {
                foreach (GameObject frame in frameList)
                {
                    if (frame.activeSelf)
                        newFrameType = Instantiate(frame);
                }
            }

            foreach (List<GameObject> triggerList in triggerGO)
            {
                foreach (GameObject trigger in triggerList)
                {
                    if (trigger.activeSelf)
                        newTriggerType = Instantiate(trigger);
                }
            }

            foreach (List<GameObject> barrelList in barrelGO)
            {
                foreach (GameObject barrel in barrelList)
                {
                    if (barrel.activeSelf)
                        newBarrelType = Instantiate(barrel);
                }
            }

            foreach (List<GameObject> scopeList in scopeGO)
            {
                foreach (GameObject scope in scopeList)
                {
                    if (scope.activeSelf)
                        newScopeType = Instantiate(scope);
                }
            }

            foreach (List<GameObject> stockList in stockGO)
            {
                foreach (GameObject stock in stockList)
                {
                    if (stock.activeSelf)
                        newStockType = Instantiate(stock);
                }
            }

            foreach (List<GameObject> magazineList in magazineGO)
            {
                foreach (GameObject magazine in magazineList)
                {
                    if (magazine.activeSelf)
                        newMagazineType = Instantiate(magazine);
                }
            }

            //PARENT GENERATED PREVIEW TO PREFAB    
            {
                if (newFrameType != null)
                    newFrameType.transform.SetParent(newGun.transform);

                if (newTriggerType != null)
                    newTriggerType.transform.SetParent(newGun.transform);

                if (newBarrelType != null)
                    newBarrelType.transform.SetParent(newGun.transform);

                if (newScopeType != null)
                    newScopeType.transform.SetParent(newGun.transform);

                if (newStockType != null)
                    newStockType.transform.SetParent(newGun.transform);

                if (newMagazineType != null)
                    newMagazineType.transform.SetParent(newGun.transform);

                GameObject prefab = PrefabUtility.CreatePrefab("Assets/ModularGunPack/GeneratedGuns/" + myTarget.gunName + ".prefab", newGun, ReplacePrefabOptions.ConnectToPrefab);
            }
        }

        if (oldFrameType != myTarget.frameType)
        {
            PreviewGun();
            oldFrameType = myTarget.frameType;
        }

        if (oldTriggerType != myTarget.triggerType)
        {
            PreviewGun();
            oldTriggerType = myTarget.triggerType;
        }

        if (oldBarrelType != myTarget.barrelType)
        {
            PreviewGun();
            oldBarrelType = myTarget.barrelType;
        }

        if (oldScopeType != myTarget.scopeType)
        {
            PreviewGun();
            oldScopeType = myTarget.scopeType;
        }

        if (oldStockType != myTarget.stockType)
        {
            PreviewGun();
            oldStockType = myTarget.stockType;
        }

        if (oldMagazineType != myTarget.magazineType)
        {
            PreviewGun();
            oldMagazineType = myTarget.magazineType;
        }

        if (oldLodType != myTarget.lodType)
        {
            PreviewGun();
            oldLodType = myTarget.lodType;
        }

        {
            PreviewGun();
        }
    }

    void OnEnable()
    {
        myTarget = (GunPackScript)target;                                                                                                    

        if (DynamicGun == null)
            DynamicGun = new GameObject("Gun Preview (Delete when done)");                                                                       
        else
        {
            for (int i = 0; i < DynamicGun.transform.childCount; i++)
            {
                DestroyImmediate(DynamicGun.transform.GetChild(i).gameObject);
            }
        }

        // Gets all the names of all the frames
        /*string[] tempFrames = Enum.GetNames(typeof(GunPackScript.frame_type_list));                                                         
        string[] tempTriggers = Enum.GetNames(typeof(GunPackScript.trigger_type_list));
        string[] tempBarrels = Enum.GetNames(typeof(GunPackScript.barrel_type_list));
        string[] tempScopes = Enum.GetNames(typeof(GunPackScript.scope_type_list));
        string[] tempStocks = Enum.GetNames(typeof(GunPackScript.stock_type_list));
        string[] tempMagazines = Enum.GetNames(typeof(GunPackScript.magazine_type_list));
        string[] tempLod = Enum.GetNames(typeof(GunPackScript.lod_type_list));*/


        //SPAWN AND DEACTIVATE GUNS ON SCRIPT ACTIVATION
        for (int i = 1; i < Enum.GetValues(typeof(GunPackScript.frame_type_list)).Length; i++)           
        {
            frameGO.Add(new List<GameObject>());

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {                
                frameGO[i - 1].Add((GameObject)Instantiate(Resources.Load("Frame0" + i + "LOD" + d, typeof(GameObject))));
                frameGO[i - 1][d - 1].SetActive(false);
                frameGO[i - 1][d - 1].transform.SetParent(DynamicGun.transform);
            }
        }

        for (int i = 1; i < Enum.GetValues(typeof(GunPackScript.trigger_type_list)).Length; i++)
        {
            triggerGO.Add(new List<GameObject>());

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                triggerGO[i - 1].Add((GameObject)Instantiate(Resources.Load("Trigger0" + i + "LOD" + d, typeof(GameObject))));
                triggerGO[i - 1][d - 1].SetActive(false);
                triggerGO[i - 1][d - 1].transform.SetParent(DynamicGun.transform);
            }
        }

        for (int i = 1; i < Enum.GetValues(typeof(GunPackScript.barrel_type_list)).Length; i++)
        {
            barrelGO.Add(new List<GameObject>());

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                barrelGO[i - 1].Add((GameObject)Instantiate(Resources.Load("Barrel0" + i + "LOD" + d, typeof(GameObject))));
                barrelGO[i - 1][d - 1].SetActive(false);
                barrelGO[i - 1][d - 1].transform.SetParent(DynamicGun.transform);
            }
        }

        for (int i = 1; i < Enum.GetValues(typeof(GunPackScript.scope_type_list)).Length; i++)
        {
            scopeGO.Add(new List<GameObject>());

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                scopeGO[i - 1].Add((GameObject)Instantiate(Resources.Load("Scope0" + i + "LOD" + d, typeof(GameObject))));
                scopeGO[i - 1][d - 1].SetActive(false);
                scopeGO[i - 1][d - 1].transform.SetParent(DynamicGun.transform);
            }
        }

        for (int i = 1; i < Enum.GetValues(typeof(GunPackScript.stock_type_list)).Length; i++)
        {
            stockGO.Add(new List<GameObject>());

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                stockGO[i - 1].Add((GameObject)Instantiate(Resources.Load("Stock0" + i + "LOD" + d, typeof(GameObject))));
                stockGO[i - 1][d - 1].SetActive(false);
                stockGO[i - 1][d - 1].transform.SetParent(DynamicGun.transform);
            }
        }

        for (int i = 1; i < Enum.GetValues(typeof(GunPackScript.magazine_type_list)).Length; i++)
        {
            magazineGO.Add(new List<GameObject>());

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                magazineGO[i - 1].Add((GameObject)Instantiate(Resources.Load("Magazine0" + i + "LOD" + d, typeof(GameObject))));
                magazineGO[i - 1][d - 1].SetActive(false);
                magazineGO[i - 1][d - 1].transform.SetParent(DynamicGun.transform);
            }
        }
    }

    private void PreviewGun()
    {
        // FRAME
        int frame = -1;
        switch (oldFrameType)
        {
            case GunPackScript.frame_type_list.rifle:
                frame = 1;
                break;
            case GunPackScript.frame_type_list.revolver:
                frame = 2;
                break;
            case GunPackScript.frame_type_list.energy:
                frame = 3;
                break;
            default: // Including none
                frame = 0;
                break;
        }

        for (int i = 1; i <= frameGO.Count; i++)

        {
            bool tempBool = (i == frame) ? true : false;

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                bool bEnable = (tempBool == true) && (d == (int)myTarget.lodType);
                frameGO[i - 1][d - 1].SetActive(bEnable);
            }
        }

        // TRIGGER
        int trigger = -1;
        switch (oldTriggerType)
        {
            case GunPackScript.trigger_type_list.standard:
                trigger = 1;
                break;
            case GunPackScript.trigger_type_list.rounded:
                trigger = 2;
                break;
            default: // Including none
                trigger = 0;
                break;
        }

        for (int i = 1; i <= triggerGO.Count; i++)

        {
            bool tempBool = (i == trigger) ? true : false;

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                bool bEnable = (tempBool == true) && (d == (int)myTarget.lodType);
                triggerGO[i - 1][d - 1].SetActive(bEnable);
            }
        }

        // BARREL
        int barrel = -1;
        switch (oldBarrelType)
        {
            case GunPackScript.barrel_type_list.shotgun:
                barrel = 1;
                break;
            case GunPackScript.barrel_type_list.rifle:
                barrel = 2;
                break;
            case GunPackScript.barrel_type_list.pistol:
                barrel = 3;
                break;
            default: // Including none
                barrel = 0;
                break;
        }

        for (int i = 1; i <= barrelGO.Count; i++)

        {
            bool tempBool = (i == barrel) ? true : false;

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                bool bEnable = (tempBool == true) && (d == (int)myTarget.lodType);
                barrelGO[i - 1][d - 1].SetActive(bEnable);
            }
        }

        // SCOPE
        int scope = -1;
        switch (oldScopeType)
        {
            case GunPackScript.scope_type_list.one:
                scope = 1;
                break;
            case GunPackScript.scope_type_list.two:
                scope = 2;
                break;
            case GunPackScript.scope_type_list.three:
                scope = 3;
                break;
            default: // Including none
                scope = 0;
                break;

        }

        for (int i = 1; i <= scopeGO.Count; i++)

        {
            bool tempBool = (i == scope) ? true : false;

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                bool bEnable = (tempBool == true) && (d == (int)myTarget.lodType);
                scopeGO[i - 1][d - 1].SetActive(bEnable);
            }
        }

        // STOCK
        int stock = -1;
        switch (oldStockType)
        {
            case GunPackScript.stock_type_list.one:
                stock = 1;
                break;
            case GunPackScript.stock_type_list.two:
                stock = 2;
                break;
            default: // Including none
                stock = 0;
                break;
        }

        for (int i = 1; i <= stockGO.Count; i++)

        {
            bool tempBool = (i == stock) ? true : false;

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                bool bEnable = (tempBool == true) && (d == (int)myTarget.lodType);
                stockGO[i - 1][d - 1].SetActive(bEnable);
            }
        }

        // MAGAZINE
        int magazine = -1;
        switch (oldMagazineType)
        {
            case GunPackScript.magazine_type_list.one:
                magazine = 1;
                break;
            case GunPackScript.magazine_type_list.two:
                magazine = 2;
                break;
            default: // Including none
                magazine = 0;
                break;
        }

        for (int i = 1; i <= magazineGO.Count; i++)

        {
            bool tempBool = (i == magazine) ? true : false;

            for (int d = 1; d <= Enum.GetValues(typeof(GunPackScript.lod_type_list)).Length; d++)
            {
                bool bEnable = (tempBool == true) && (d == (int)myTarget.lodType);
                magazineGO[i - 1][d - 1].SetActive(bEnable);
            }
        }
    }     
}