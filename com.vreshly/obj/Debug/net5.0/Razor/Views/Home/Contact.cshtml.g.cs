#pragma checksum "/Users/sasmuelezeala/Documents/SammyProjects/vreshly/com.vreshly/Views/Home/Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a2a27b0ae2ff99195a17a42d14e1d0bf0ecd614b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Contact), @"mvc.1.0.view", @"/Views/Home/Contact.cshtml")]
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
#line 1 "/Users/sasmuelezeala/Documents/SammyProjects/vreshly/com.vreshly/Views/_ViewImports.cshtml"
using com.vreshly;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/sasmuelezeala/Documents/SammyProjects/vreshly/com.vreshly/Views/_ViewImports.cshtml"
using com.vreshly.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2a27b0ae2ff99195a17a42d14e1d0bf0ecd614b", @"/Views/Home/Contact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c8f6f825c60387ada69828995accb43cff6a621", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("contact-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("accept-charset", new global::Microsoft.AspNetCore.Html.HtmlString("UTF-8"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("contact-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "/Users/sasmuelezeala/Documents/SammyProjects/vreshly/com.vreshly/Views/Home/Contact.cshtml"
  
    ViewData["Title"] = "Contact";
    Layout = "~/Views/Shared/_LayoutFrontend.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""contact-us-area"">
    <div class=""container container-default-2 custom-area"">
        <div class=""row"">
            <div class=""col-lg-4 col-md-6 col-custom"">
                <div class=""contact-info-item"">
                    <div class=""con-info-icon"">
                        <i class=""ion-ios-location-outline""></i>
                    </div>
                    <div class=""con-info-txt"">
                        <h4>Our Location</h4>
                        <p><span class=""contact-address""></span>, <span class=""contact-city""></span>,<br/> <span class=""contact-state""></span>, <span class=""contact-country""></span></p>
                    </div>
                </div>
            </div>
            <div class=""col-lg-4 col-md-6 col-custom"">
                <div class=""contact-info-item"">
                    <div class=""con-info-icon"">
                        <i class=""ion-iphone""></i>
                    </div>
                    <div class=""con-info-txt"">
                        <h4>Contact us A");
            WriteLiteral(@"nytime</h4>
                        <p>Mobile: <span class=""contact-phone""></span><br/></p>
                    </div>
                </div>
            </div>
            <div class=""col-lg-4 col-md-12 col-custom text-align-center"">
                <div class=""contact-info-item"">
                    <div class=""con-info-icon"">
                        <i class=""ion-ios-email-outline""></i>
                    </div>
                    <div class=""con-info-txt"">
                        <h4>Support Overall</h4>
                        <p><span class=""contact-phone""></span> <br>  <a href=""mailto:contact@vreshly.com"" class=""contact-email""></a></p>
                    </div>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-12 col-custom"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2a27b0ae2ff99195a17a42d14e1d0bf0ecd614b6735", async() => {
                WriteLiteral(@"
                    <div class=""comment-box mt-5"">
                        <h5 class=""text-uppercase"">Get in Touch</h5>
                        <div class=""row mt-3"">
                            <div class=""col-md-6 col-custom"">
                                <div class=""input-item mb-4"">
                                    <input class=""border rounded-0 w-100 input-area name"" type=""text"" name=""con_name"" id=""con_name"" placeholder=""Name"">
                                </div>
                            </div>
                            <div class=""col-md-6 col-custom"">
                                <div class=""input-item mb-4"">
                                    <input class=""border rounded-0 w-100 input-area email"" type=""email"" name=""con_email"" id=""con_email"" placeholder=""Email"">
                                </div>
                            </div>
                            <div class=""col-12 col-custom"">
                                <div class=""input-item mb-4"">
                             ");
                WriteLiteral(@"       <input class=""border rounded-0 w-100 input-area email"" type=""text"" name=""con_content"" id=""con_content"" placeholder=""Subject"">
                                </div>
                            </div>
                            <div class=""col-12 col-custom"">
                                <div class=""input-item mb-4"">
                                    <textarea cols=""30"" rows=""5"" class=""border rounded-0 w-100 custom-textarea input-area"" name=""con_message"" id=""con_message"" placeholder=""Message""></textarea>
                                </div>
                            </div>
                            <div class=""col-12 col-custom mt-40"">
                                <button type=""submit"" id=""submit"" name=""submit"" class=""btn obrien-button primary-btn rounded-0 mb-0"">Send A Message</button>
                            </div>
");
                WriteLiteral("                        </div>\n                    </div>\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n            </div>\n        </div>\n    </div>\n</div>\n<!-- Contact Us Area End Here -->\n<!-- Google Maps -->\n<div class=\"google-map-area\">\n    <div id=\"contacts\" class=\"map-area\">\n        <div id=\"googleMap\"></div>\n    </div>\n</div>");
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
