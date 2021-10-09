/// <summary>
/// Interface for damagable object
/// </summary>
public interface IDamagable
{
    /// <summary>
    /// Take the damage
    /// </summary>
    /// <param name="damage">Point of damage</param>
    void TakeDamage(float damage);
}
