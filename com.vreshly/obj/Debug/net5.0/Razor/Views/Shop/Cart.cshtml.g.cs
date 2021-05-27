#pragma checksum "/Users/esc/Documents/Projects/com.vreshly/com.vreshly/Views/Shop/Cart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a4d389c7e2cbbd343a460fa6e444ea55a5946ea3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_Cart), @"mvc.1.0.view", @"/Views/Shop/Cart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/esc/Documents/Projects/com.vreshly/com.vreshly/Views/_ViewImports.cshtml"
using com.vreshly;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/esc/Documents/Projects/com.vreshly/com.vreshly/Views/_ViewImports.cshtml"
using com.vreshly.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a4d389c7e2cbbd343a460fa6e444ea55a5946ea3", @"/Views/Shop/Cart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c8f6f825c60387ada69828995accb43cff6a621", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop_Cart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(" d-block d-md-flex"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/esc/Documents/Projects/com.vreshly/com.vreshly/Views/Shop/Cart.cshtml"
  
    ViewData["Title"] = "Cart";
    Layout = "~/Views/Shared/_LayoutFrontend.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""cart-main-wrapper mt-no-text mb-no-text"">
    <div class=""container container-default-2 custom-area"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <!-- Cart Table Area -->
                <div class=""cart-table table-responsive"">
                    <table class=""table table-bordered"">
                        <thead>
                            <tr>
                                <th class=""pro-thumbnail"">Image</th>
                                <th class=""pro-title"">Product</th>
                                <th class=""pro-price"">Price</th>
                                <th class=""pro-quantity"">Quantity</th>
                                <th class=""pro-subtotal"">Total</th>
                                <th class=""pro-remove"">Remove</th>
                            </tr>
                        </thead>
                        <tbody id=""cart-table"">
                            
                        </tbody>
                    </table>
             ");
            WriteLiteral("   </div>\n                <!-- Cart Update Option -->\n                <div class=\"cart-update-option d-block d-md-flex justify-content-between\">\n                    <div class=\"apply-coupon-wrapper\">\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a4d389c7e2cbbd343a460fa6e444ea55a5946ea35691", async() => {
                WriteLiteral("\n                            <input type=\"text\" placeholder=\"Enter Your Coupon Code\" required />\n                            <button class=\"btn obrien-button primary-btn\">Apply Coupon</button>\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                    <div class=""cart-update mt-sm-16"">
                        <a id=""update-cart"" class=""btn obrien-button primary-btn"">Update Cart</a>
                    </div>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-lg-5 ml-auto"">
                <!-- Cart Calculation Area -->
                <div class=""cart-calculator-wrapper"">
                    <div class=""cart-calculate-items"">
                        <h3>Cart Totals</h3>
                        <div class=""table-responsive"">
                            <table class=""table"">
                                <tr>
                                    <td>Sub Total</td>
                                    <td>NGN <span class=""sub-total""></span></td>
                                </tr>
");
            WriteLiteral(@"                                <tr class=""total"">
                                    <td>Total</td>
                                    <td class=""total-amount"">300</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <a href=""checkout.html"" class=""btn obrien-button primary-btn d-block"">Proceed To Checkout</a>
                </div>
            </div>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
