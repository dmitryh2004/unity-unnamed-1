using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField] Transform backEngine, frontEngine;
    [SerializeField] List<ParticleSystem> backEngineParticles = new(), frontEngineParticles = new();
    [SerializeField] Transform door;

    Animator shipAnim, doorAnim, backEngineAnim, frontEngineAnim;
    bool doorClosed = false, backEngineEnabled = false, frontEngineEnabled = false;

    private void Start()
    {
        shipAnim = GetComponent<Animator>();
        doorAnim = door.GetComponent<Animator>();
        backEngineAnim = backEngine.GetComponent<Animator>();
        frontEngineAnim = frontEngine.GetComponent<Animator>();

        StopEngine(0);
        StopEngine(1);
    }

    void ChangeDoorState()
    {
        doorAnim.SetBool("Closed", doorClosed);
    }

    public void CloseDoor()
    {
        doorClosed = true;
        ChangeDoorState();
    }

    public void OpenDoor()
    {
        doorClosed = false;
        ChangeDoorState();
    }

    void ChangeEngineState(int engine)
    {
        if (engine == 0)
        {
            foreach(ParticleSystem ps in backEngineParticles)
            {
                if (backEngineEnabled) ps.Play(); else ps.Stop();
            }
        }
        else
        {
            foreach (ParticleSystem ps in frontEngineParticles)
            {
                if (frontEngineEnabled) ps.Play(); else ps.Stop();
            }
        }
    }

    public void StopEngine(int engine)
    {
        if (engine == 0) backEngineEnabled = false; else frontEngineEnabled = false;
        ChangeEngineState(engine);
    }

    public void StartEngine(int engine)
    {
        if (engine == 0) backEngineEnabled = true; else frontEngineEnabled = true;
        ChangeEngineState(engine);
    }

    public void LiftOff()
    {
        StartCoroutine(LiftOffAnimation());
    }

    IEnumerator LiftOffAnimation()
    {
        shipAnim.SetTrigger("LiftOff"); //запуск анимации взлета

        yield return new WaitForSeconds(5f); //длительность взлета

        backEngineAnim.SetInteger("State", 2); //вращение двигателей

        yield return new WaitForSeconds(1f); //длительность смены угла двигателей

        frontEngineAnim.SetInteger("State", 2); //вращение двигателей

        yield return new WaitForSeconds(1f); //длительность смены угла двигателей

        shipAnim.SetTrigger("FlyAway");
    }
}
