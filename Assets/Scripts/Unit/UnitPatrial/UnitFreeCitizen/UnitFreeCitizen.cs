using UnityEngine;

public class UnitFreeCitizen : UnitCitizen
{
    private Item _item;

    private bool _forgeHaveItem;

    protected override void StartCitizenUnit()
    {
        InitState(new FollowObjectState(this, GetComponent<Animator>()), new MoveState(this, GetComponent<Animator>()));        
    }

    private void Update()
    {
        _forgeHaveItem = Countr.ItemsInShop.Count > 0 && !_item;

        if (_forgeHaveItem)
        {
            if (GetTaskState() is FollowObjectState followObject)
            {
                followObject.LookObject(Countr.ForgePosition);

                GetStateMachineUnit().ChangeState(GetTaskState());
            }
        }
        else
        {
                GetStateMachineUnit().ChangeState(GetMoveState());
        }

        GetStateMachineUnit().Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Forge forge = other.gameObject.GetComponent<Forge>();

        if (forge != null && _forgeHaveItem)
        {
            _item = forge.DeleteItem();

            Countr.UpgradeUnit(transform, this, _item);
        }
    }
}
