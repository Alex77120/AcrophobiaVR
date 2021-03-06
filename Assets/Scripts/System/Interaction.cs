using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Interaction : MonoBehaviour
{
    private float waitingTime = 2f;
    private int fenceType;

    [SerializeField] InputActionReference activate;
    [SerializeField] InputActionReference select;
    [SerializeField] InputActionReference handGrip;

    [SerializeField] XRRayInteractor _RayInteractor;
    [SerializeField] Canvas menu;
    [SerializeField] GameObject balcony;
    [SerializeField] GameManager gameManager;


    void Awake()
    {
        activate.action.performed += ButtonAction;
        select.action.performed += ButtonMenu;
        handGrip.action.performed += SafeZoneActivation;

        gameManager = gameManager.GetComponent<GameManager>();
    }

         ////////////////////// CONTROLLER INPUTS //////////////////////////

    private void SafeZoneActivation(InputAction.CallbackContext obj)
    {
        if (gameManager.fader.GetComponent<Fader>().newColor2.a == 1)
        {
            gameManager.fader.GetComponent<Fader>().FadeOut();
        }
        if (gameManager.fader.GetComponent<Fader>().newColor2.a == 0)
        {
            gameManager.fader.GetComponent<Fader>().FadeIn();
        }
    }

    private void ButtonAction(InputAction.CallbackContext obj)
    {
        if (_RayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit ray))
        {
            if (ray.transform.name == "Button")
            {
                if (ray.transform.GetComponent<Animator>().GetBool("isOn") == false)
                {
                    ray.transform.GetComponentInParent<CabinControlPanel>().ButtonActivation(ray.transform);
                }
            }
        }
    }

    private void ButtonMenu(InputAction.CallbackContext obj)
    {
        if (!gameManager.safeMode)
        {
            if (menu.gameObject.activeSelf)
            {
                menu.gameObject.SetActive(false);
            }
            else
            {
                menu.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
            ////////////////////// KEYBOARD INPUTS //////////////////////////
            ///

        if (gameManager.isInputEnabled)
        {
            // SAFE MODE

            if (Input.GetKeyDown("s"))
            {
                gameManager.SafeModeActivation();
            }

            if (gameManager.safeMode)
            {
                // FLOOR SELECTION
                if (Input.GetKeyDown("0"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 0));
                }
                if (Input.GetKeyDown("1"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 1));
                }
                if (Input.GetKeyDown("2"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 2));
                }
                if (Input.GetKeyDown("3"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 3));
                }
                if (Input.GetKeyDown("4"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 4));
                }
                if (Input.GetKeyDown("5"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 5));
                }
                if (Input.GetKeyDown("6"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 6));
                }
                if (Input.GetKeyDown("7"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 7));
                }
                if (Input.GetKeyDown("8"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 8));
                }
                if (Input.GetKeyDown("9"))
                {
                    StartCoroutine(gameManager.ChangeFloorFromKeyboard(waitingTime, 9));
                }

                // FENCE
                //SHOW FULL FENCE
                if (Input.GetKeyDown("a"))
                {
                    fenceType = 0;
                    if (balcony.GetComponent<Balcony>().fences.GetComponent<Fences>().CheckFenceState(fenceType))
                    {
                        StartCoroutine(gameManager.ChangeFenceFromKeyboard(waitingTime, fenceType));
                    }
                    else
                    {
                        Debug.LogError("Full fence already shown");
                    }
                }

                //SHOW LIGHT FENCE
                if (Input.GetKeyDown("z"))
                {
                    fenceType = 1;
                    if (balcony.GetComponent<Balcony>().fences.GetComponent<Fences>().CheckFenceState(fenceType))
                    {
                        StartCoroutine(gameManager.ChangeFenceFromKeyboard(waitingTime, fenceType));
                    }
                    else
                    {
                        Debug.LogError("Light fence already shown");
                    }
                }

                //HIDE FENCE
                if (Input.GetKeyDown("e"))
                {
                    fenceType = 2;
                    if (balcony.GetComponent<Balcony>().fences.GetComponent<Fences>().CheckFenceState(fenceType))
                    {
                        StartCoroutine(gameManager.ChangeFenceFromKeyboard(waitingTime, fenceType));
                    }
                    else
                    {
                        Debug.LogError("Fence already hidden");
                    }
                }

                // PLANK
                if (Input.GetKeyDown("p"))
                {
                    StartCoroutine(gameManager.ActivePlankFromKeyboard(waitingTime));
                }
            }
        } 
    }
}
    
