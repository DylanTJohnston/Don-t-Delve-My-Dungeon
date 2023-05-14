using UnityEngine;

public class shop : MonoBehaviour
{

    BuildManager buildManager;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret ()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseAnotherTurret ()
    {
        Debug.Log("AnotherTurret Selected");
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab);
    }

}
