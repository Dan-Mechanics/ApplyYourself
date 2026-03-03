using UnityEngine;

namespace ApplyYourself
{
    public class DestroyIf : Placeholder
    {
        [SerializeField] private Ending ending = default;
        [SerializeField] private Object target = default;

        public override void SetAs(Ending ending)
        {
            ending = Utils.Filter(ending, endingOverrides);
            if (target == null)
            {
                gameObject.SetActive(this.ending != ending);
                if (!gameObject.activeSelf)
                    Destroy(gameObject);
            }
            else
            {
                if (this.ending == ending)
                    Destroy(target);
            }
        }
    }
}
