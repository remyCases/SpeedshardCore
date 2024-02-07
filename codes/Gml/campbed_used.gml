if (!global.campbed_not_destroyed)
{
    with (owner)
    {
        if (object_index == o_campbed_crafted)
            instance_destroy()
    }
}