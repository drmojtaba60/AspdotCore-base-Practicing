using Microsoft.OpenApi.Attributes;
using System.ComponentModel;

namespace webapi_base.Enums
{
    public enum Category
    {
       [Display("پیش فرض")]
        [Description("گزینه پیش فرض")]
       Default=0,
        [Display("شخصی")]
        [Description("گزینه شخص مختص خود کاربر")]
        Personnel,
        [Display("سازمانی")]
        [Description("گزینه مربوط به سازمان")]
        Organization,
        [Display("پروژه ای")]
        Project,
        [Display("خانواده ای")]
        Family
}
}
