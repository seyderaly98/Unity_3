using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;
    private PlaySound playSound;

    void Start()
    {
        character = GetComponentInParent<Character>();
        playSound = character.GetComponentInParent<PlaySound>();
    }

    void ShootEnd()
    {
        character.SetState(Character.State.Idle);
    }

    void AttackEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
    }

    void SoundAttack()
    {
        playSound.Play("ShootHit");
    }

    void PunchEnd()
    {
        character.SetState(Character.State.RunningFromEnemy);
    }

    void DoDamage()
    {
        Character targetCharacter = character.target.GetComponent<Character>();
        targetCharacter.DoDamage();
    }
}
