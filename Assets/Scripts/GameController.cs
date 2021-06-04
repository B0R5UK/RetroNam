using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isPaused;
    //singleton
    public static GameController Instance;
    //UI
    [SerializeField] Text ammoText;
    [SerializeField] Text healthText;
    [SerializeField] GameObject hitPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] GameObject DiedPanel;
    
    public Player Player;



    //Graphics
    public GameObject bulletHole;
    public GameObject bullet;
    public GameObject blood;
    public Image currentWeaponImage;
    public Sprite pistolImage;
    public Sprite shotgunImage;
    public Sprite fistImage;


    public AudioSource globalAudioSource;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Instance = this;
        hitPanel.SetActive(false);
        UpdateAmmoText();
        UpdateWeaponIcon();
        UpdateHealthText();
    }


    public void ToggleDiePanel()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        HUDPanel.SetActive(false);
        DiedPanel.SetActive(true);
        hitPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
                Pause(false);
            else
                Pause(true);


        UpdateAmmoText();
        UpdateHealthText();
    }

    public void UpdateWeaponIcon()
    {
        Weapon type = Player.currentWeapon;

        if (type.type == WeaponType.Pistol)
            currentWeaponImage.sprite = pistolImage;
        else if (type.type == WeaponType.Shotgun)
            currentWeaponImage.sprite = shotgunImage;
        else if (type.type == WeaponType.Fist)
            currentWeaponImage.sprite = fistImage;
    }

    public void UpdateHealthText()
    {
        healthText.text = Player.currentHealth.ToString();
    }

    public void UpdateAmmoText()
    {
        if (Player.currentWeapon.type == WeaponType.Fist)
            ammoText.text = "";
        else
            ammoText.text = Player.currentWeapon.currentMagAmmo + "/" + Player.currentWeapon.currentAmmo;
    }

    public void TakeHit()
    {
        StopAllCoroutines();
        StartCoroutine(RedScreen());
    }

    IEnumerator RedScreen()
    {
        hitPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitPanel.SetActive(false);
    }

    public void Pause(bool state)
    {
        if (state)
        {
            HUDPanel.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            HUDPanel.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
            
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

}
