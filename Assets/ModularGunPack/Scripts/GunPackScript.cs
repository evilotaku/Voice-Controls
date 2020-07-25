using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPackScript : MonoBehaviour
{
    public string gunName = "Death Machine";
    public Vector3 spawnPoint;

    public frame_type_list frameType = frame_type_list.none;

    public enum frame_type_list
    {
        rifle = 0,
        revolver,
        energy,       
        none
    }

    public trigger_type_list triggerType = trigger_type_list.none;

    public enum trigger_type_list
    {
        standard,
        rounded,
        none
    }

    public barrel_type_list barrelType = barrel_type_list.none;

    public enum barrel_type_list
    {
        shotgun,
        rifle,
        pistol,
        none
    }

    public scope_type_list scopeType = scope_type_list.none;

    public enum scope_type_list
    {
        one,
        two,
        three,
        none
    }

    public stock_type_list stockType = stock_type_list.none;

    public enum stock_type_list
    {
        one,
        two,
        none
    }

    public magazine_type_list magazineType = magazine_type_list.none;

    public enum magazine_type_list
    {
        one,
        two,
        none
    }

    public lod_type_list lodType = lod_type_list.low;

    public enum lod_type_list
    {
        high = 1,
        low
    }

}