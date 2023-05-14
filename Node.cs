using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Color startColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown ()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: display on screen");
            return;
        }

        //Build a Turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

    }


    void OnMouseEnter ()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit ()
    {
        rend.material.color = startColor;
    }



}
