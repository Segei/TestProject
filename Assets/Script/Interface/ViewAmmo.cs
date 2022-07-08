using Assets.Script.Weapon;
using TMPro;
using UnityEngine;

public class ViewAmmo : MonoBehaviour
{
    public Weapons Weapons => _weapon;
    
    [SerializeField] private TMP_Text text;
    [SerializeField] private Weapons _weapon;
    


    public void UpdateAmmo()
    {
        text.text = _weapon.AmmoInMagazine + "/" + _weapon.TotalAmmo;   
    }

    public void SetWeapon(Weapons weapon)
    {
        _weapon = weapon;
        UpdateAmmo();
        _weapon.changeAmmo += UpdateAmmo;
    }

}
