using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfo : MonoBehaviour
{
    [SerializeField] Select selection;
    [SerializeField] Hover hovering;

    [SerializeField] TMPro.TextMeshProUGUI title;
    [SerializeField] Slider damage;
    [SerializeField] Slider range;
    [SerializeField] Slider reload;

    private void Update()
    {
        
        foreach (Transform t in transform) { t.gameObject.SetActive(true); }
        if (hovering.hover != null && hovering.hover.TryGetComponent<TurretUI>(out TurretUI tUI))
        {
            Turret target = tUI.turret;
            if (target == null)
            {
                foreach (Transform t in transform) { t.gameObject.SetActive(false); }
                return;
            }

            title.text = target.title;
            damage.value = 5 - target.damage;
            range.value = 5 - target.range;
            int reloadValue = (int)(target.reloadTime * 2);
            reload.value = reloadValue;
        }
        else if (selection.selected != null && selection.selected.TryGetComponent<TurretManager>(out TurretManager tManager))
        {
            Turret target = tManager.GetCurrent();
            if (target == null) 
            { 
                foreach (Transform t in transform) { t.gameObject.SetActive(false); }
                return;
            }
            title.text = target.title;
            damage.value = 5 - target.damage;
            range.value = 5 - target.range;
            int reloadValue = (int)(target.reloadTime * 2) + 1;
            reload.value = reloadValue;
        }
        else
        {
            foreach(Transform t in transform) { t.gameObject.SetActive(false); }
        }
    }
}
