#pragma checksum "/Users/sasmuelezeala/Documents/SammyProjects/vreshly/com.vreshly/Views/Account/Information.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "439284a8bafce0d459cad8be5ef8293aa05ee5d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Information), @"mvc.1.0.view", @"/Views/Account/Information.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"439284a8bafce0d459cad8be5ef8293aa05ee5d7", @"/Views/Account/Information.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c8f6f825c60387ada69828995accb43cff6a621", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Information : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/account.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/sasmuelezeala/Documents/SammyProjects/vreshly/com.vreshly/Views/Account/Information.cshtml"
  
    ViewData["Title"] = "Account Information";
    Layout = "~/Views/Shared/_LayoutFrontend.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!-- my account wrapper start -->
<div class=""my-account-wrapper mt-no-text mb-no-text"">
    <div class=""container container-default-2 custom-area"">
        <div class=""row"">
            <div class=""col-lg-12 col-custom"">
                <!-- My Account Page Start -->
                <div class=""myaccount-page-wrapper"">
                    <!-- My Account Tab Menu Start -->
                    <div class=""row"">
                        <div class=""col-lg-3 col-md-4 col-custom"">
                            <div class=""myaccount-tab-menu nav"" role=""tablist"">
                                <a href=""#dashboad"" class=""active"" data-toggle=""tab"">
                                    <i class=""fa fa-dashboard""></i>
                                    Dashboard
                                </a>
                                <a href=""#orders"" data-toggle=""tab"">
                                    <i class=""fa fa-cart-arrow-down""></i>
                                    Orders
                                </a>
 ");
            WriteLiteral(@"                               <a href=""#download"" data-toggle=""tab"">
                                    <i class=""fa fa-cloud-download""></i>
                                    Download
                                </a>
                                <a href=""#payment-method"" data-toggle=""tab"">
                                    <i class=""fa fa-credit-card""></i>
                                    Payment
                                    Method
                                </a>
                                <a href=""#address-edit"" data-toggle=""tab"">
                                    <i class=""fa fa-map-marker""></i>
                                    address
                                </a>
                                <a href=""#account-info"" data-toggle=""tab"">
                                    <i class=""fa fa-user""></i> Account
                                    Details
                                </a>
                                <a href=""javascript:void()"" class=""logout-bt");
            WriteLiteral(@"n""><i class=""fa fa-sign-out""></i> Logout</a>
                            </div>
                        </div>
                        <!-- My Account Tab Menu End -->
                        <!-- My Account Tab Content Start -->
                        <div class=""col-lg-9 col-md-8 col-custom"">
                            <div class=""tab-content"" id=""myaccountContent"">
                                <!-- Single Tab Content Start -->
                                <div class=""tab-pane fade show active"" id=""dashboad"" role=""tabpanel"">
                                    <div class=""myaccount-content"">
                                        <h3>Dashboard</h3>
                                        <div class=""welcome"">
                                            <p>Hello, <span id=""name"">Alex Aya</span> </p>
                                        </div>
                                        <p class=""mb-0"">From your account dashboard. you can easily check & view your recent orders, manage your shipping an");
            WriteLiteral(@"d billing addresses and edit your password and account details.</p>
                                    </div>
                                </div>
                                <!-- Single Tab Content End -->
                                <!-- Single Tab Content Start -->
                                <div class=""tab-pane fade"" id=""orders"" role=""tabpanel"">
                                    <div class=""myaccount-content"" id=""order-list"">
                                        <h3>Orders</h3>
                                        <div class=""myaccount-table table-responsive text-center"">
                                            <table class=""table table-bordered"">
                                                <thead class=""thead-light"">
                                                    <tr>
                                                        <th>Order</th>
                                                        <th>Date</th>
                                                        <th>St");
            WriteLiteral(@"atus</th>
                                                        <th>Total</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody id=""order-history"">
");
            WriteLiteral(@"                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class=""myaccount-content"" id=""order-details"" style=""display:none"">
                                    <h3><span><a href=""javascript:void()"" class=""close"">x</a></span>  Order Details</h3>
                                    <div class=""col-md-12"">
                                        <section>
                                            <div class=""col-lg-12"">
                                                <div class=""pull-left"">
                                                <p class=""pull-left"">Order No. <span class=""order-id""></span></p><div class=""clearfix""></div>
                                                <p class=""pull-left""><span class=""total-items""></span>item(s)</p><div class=""clearfix""></div>
                                                <p class=""pul");
            WriteLiteral(@"l-left"">placed on <span class=""order-date""></span></p><div class=""clearfix""></div>
                                                <p class=""pull-left"">Total: <span class=""sub-total""></span></p><div class=""clearfix""></div>
                                                </div>
                                                <div class=""clearfix""></div>
                                            </div>
                                        </section>
                                        <div class=""col-md-12"">
                                            <br/>
                                            <section>
                                                <h5 style=""text-transform:uppercase; text-align: left; font-size: small;"" >Items in your order list</h5>
                                                <div class=""row"" id=""items"">
                                                    
                                                </div>
                                            </section>
      ");
            WriteLiteral(@"                                  </div>
                                    
                                        <div class=""col-md-12"" style=""padding: 5px 0 2px 5px;display: flex; flex-direction: row;"">
                                            <div class=""col-md-6"" style=""margin-right: 3px;"">
                                                <div class=""card"">
                                                    <div class=""card-header"">
                                                        <span style=""text-transform:uppercase;"">Payment Information</span>
                                                    </div>
                                                    <div class=""card-body"" style=""text-align: left;"">
                                                            <div class=""card-title""><strong>Payment Method</strong></div>
                                                            <div class=""card-text payment-method""></div>
                                                            <br/>");
            WriteLiteral(@"
                                                            <div class=""card"" style=""width: 18rem;"">
                                                                <div class=""card-header"">
                                                                    <span");
            BeginWriteAttribute("style", " style=\"", 9617, "\"", 9625, 0);
            EndWriteAttribute();
            WriteLiteral(@"> Payment Details</span>
                                                                </div>
                                                                <ul class=""list-group list-group-flush"">
                                                                <li class=""list-group-item"">Items Total: <span class=""item-total""></span></li>
                                                                <li class=""list-group-item"">Shipping Fee: <span class=""shipping-fee""></span></li>
                                                                <li class=""list-group-item"">Total: <span class=""sub-total""></span></li>
                                                                </ul>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class=""col-md-6"" style=""margin-left: 0;"">
     ");
            WriteLiteral(@"                                           <div class=""card"">
                                                    <div class=""card-header"">
                                                        <span style=""text-transform:uppercase;"">Delivery Information</span>
                                                    </div>
                                                    <div class=""card-body"" style=""text-align: left;"">
                                                            <div class=""card-title""><strong>Delivery Method</strong></div>
                                                            <div class=""card-text""><span class=""delivery-method""></span></div>
                                                            <br/><br/>
                                                            <div class=""card-title""><strong>Delivery Address</strong></div>
                                                            <div class=""card-text""  style=""overflow-wrap:break-word; word-wrap: break-word;hyphens: auto;""");
            WriteLiteral(@">
                                                                <div style=""overflow-wrap:break-word; word-wrap: break-word;hyphens: auto;""><span class=""delivery-address""></span> </div>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
</div>
                                </div>
                                <!-- Single Tab Content End -->
                                <!-- Single Tab Content Start -->
                                <div class=""tab-pane fade"" id=""download"" role=""tabpanel"">
                                    <div class=""myaccount-content"">
                                        <h3>Downloads</h3>
                                        <div class=""myaccount-table table-responsive text-center"">
                      ");
            WriteLiteral(@"                      <table class=""table table-bordered"">
                                                <thead class=""thead-light"">
                                                    <tr>
                                                        <th>Product</th>
                                                        <th>Date</th>
                                                        <th>Expire</th>
                                                        <th>Download</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Haven - Free Real Estate PSD Template</td>
                                                        <td>Aug 22, 2018</td>
                                                        <td>Yes</td>
                                                        <td><a hr");
            WriteLiteral(@"ef=""#"" class=""btn obrien-button-2 primary-color rounded-0""><i class=""fa fa-cloud-download mr-2""></i>Download File</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>HasTech - Profolio Business Template</td>
                                                        <td>Sep 12, 2018</td>
                                                        <td>Never</td>
                                                        <td><a href=""#"" class=""btn obrien-button-2 primary-color rounded-0""><i class=""fa fa-cloud-download mr-2""></i>Download File</a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <!-- Single Tab Co");
            WriteLiteral(@"ntent End -->
                                <!-- Single Tab Content Start -->
                                <div class=""tab-pane fade"" id=""payment-method"" role=""tabpanel"">
                                    <div class=""myaccount-content"">
                                        <h3>Payment Method</h3>
                                        <p class=""saved-message"">You Can't Saved Your Payment Method yet.</p>
                                    </div>
                                </div>
                                <!-- Single Tab Content End -->
                                <!-- Single Tab Content Start -->
                                <div class=""tab-pane fade"" id=""address-edit"" role=""tabpanel"">
                                    <div class=""myaccount-content"" id=""billing-info"">
                                        <h3>Billing Address</h3>
                                        <address>
                                            <p><strong class=""fullname""></strong></p>
             ");
            WriteLiteral(@"                               <p>
                                                <span class=""street""></span>, <span class=""city""></span> <br>
                                                <span class=""state""></span>, <span class=""zip-code""></span>
                                            </p>
                                            <p>Mobile: <span class=""phone""></span></p>
                                        </address>
                                        <a href=""#editAddressModal"" class=""btn obrien-button-2 primary-color rounded-0"" data-toggle=""modal""><i class=""fa fa-edit mr-2""></i>Edit Address</a>
                                    </div>
                                </div>
                                <!-- Single Tab Content End -->
                                <!-- Single Tab Content Start -->
                                <div class=""tab-pane fade"" id=""account-info"" role=""tabpanel"">
                                    <div class=""myaccount-content"">
                      ");
            WriteLiteral("                  <h3>Account Details</h3>\n                                        <div class=\"account-details-form\">\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "439284a8bafce0d459cad8be5ef8293aa05ee5d720366", async() => {
                WriteLiteral(@"
                                                <div class=""single-input-item mb-3"">
                                                    <label for=""display-name"" class=""required mb-1"">Display Name</label>
                                                    <input type=""text"" id=""display-name"" placeholder=""Display Name"" />
                                                </div>
                                                <div class=""single-input-item mb-3"">
                                                    <label for=""email"" class=""required mb-1"">Email Addres</label>
                                                    <input type=""email"" id=""email"" placeholder=""Email Address"" />
                                                </div>
                                                <fieldset>
                                                    <legend>Password change</legend>
                                                    <div class=""single-input-item mb-3"">
                                          ");
                WriteLiteral(@"              <label for=""current-pwd"" class=""required mb-1"">Current Password</label>
                                                        <input type=""password"" id=""current-pwd"" placeholder=""Current Password"" />
                                                    </div>
                                                    <div class=""row"">
                                                        <div class=""col-lg-6 col-custom"">
                                                            <div class=""single-input-item mb-3"">
                                                                <label for=""new-pwd"" class=""required mb-1"">New Password</label>
                                                                <input type=""password"" id=""new-pwd"" placeholder=""New Password"" />
                                                            </div>
                                                        </div>
                                                        <div class=""col-lg-6 col-custom"">
              ");
                WriteLiteral(@"                                              <div class=""single-input-item mb-3"">
                                                                <label for=""confirm-pwd"" class=""required mb-1"">Confirm Password</label>
                                                                <input type=""password"" id=""confirm-pwd"" placeholder=""Confirm Password"" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                                <div class=""single-input-item single-item-button"">
                                                    <button class=""btn obrien-button primary-btn"">Save Changes</button>
                                                </div>
                                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                        </div>
                                    </div>
                                </div> <!-- Single Tab Content End -->
                            </div>
                        </div> <!-- My Account Tab Content End -->
                    </div>
                </div> <!-- My Account Page End -->
            </div>
        </div>
    </div>
</div>

<div class=""modal fade obrien-modal"" id=""editAddressModal"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"">
        <div class=""modal-dialog modal-dialog-centered"" role=""document"">
            <div class=""modal-content"">
                <button type=""button"" class=""close close-button"" data-dismiss=""modal"" aria-label=""Close"">
                    <span class=""close-icon"" aria-hidden=""true"">x</span>
                </button>
                <div class=""modal-body"">
                    <div class=""myaccount-content"">
                        <h3>Edit Address</h3>
                        <div class=""account-details-form"">
     ");
            WriteLiteral("                       ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "439284a8bafce0d459cad8be5ef8293aa05ee5d725881", async() => {
                WriteLiteral(@"
                                <div class=""row"">
                                    <div class=""col-lg-6 col-custom"">
                                        <div class=""single-input-item mb-3"">
                                            <label for=""first-name"" class=""required mb-1"">First Name</label>
                                            <input type=""text"" id=""first-name"" placeholder=""First Name"" />
                                        </div>
                                    </div>
                                    <div class=""col-lg-6 col-custom"">
                                        <div class=""single-input-item mb-3"">
                                            <label for=""last-name"" class=""required mb-1"">Last Name</label>
                                            <input type=""text"" id=""last-name"" placeholder=""Last Name"" />
                                        </div>
                                    </div>
                                </div>
                                ");
                WriteLiteral(@"<div class=""row"">
                                    <div class=""col-lg-6 col-custom"">
                                        <div class=""single-input-item mb-3"">
                                        <label for=""street"" class=""required mb-1"">Street</label>
                                        <input type=""text"" id=""street"" placeholder=""Street"" />
                                        </div>
                                    </div>
                                    <div class=""col-lg-6 col-custom"">
                                    <div class=""single-input-item mb-3"">
                                            <label for=""city"" class=""required mb-1"">City</label>
                                            <input type=""text"" id=""city"" placeholder=""City"" />
                                        </div>
                                    </div>
                                </div>
                                <div class=""row"">
                                    <div class=""col-lg-4 col-cu");
                WriteLiteral(@"stom"">
                                        <div class=""single-input-item mb-3"">
                                            <label for=""state"" class=""required mb-1"">State</label>
                                            <input type=""text"" id=""state"" placeholder=""State"" />
                                        </div>
                                    </div>
                                    <div class=""col-lg-4 col-custom"">
                                            <div class=""single-input-item mb-3"">
                                            <label for=""zip-code"" class=""required mb-1"">Zip Code</label>
                                            <input type=""text"" id=""zip-code"" placeholder=""Zip Code"" />
                                        </div>
                                    </div>
                                    <div class=""col-lg-4 col-custom"">
                                        <div class=""single-input-item mb-3"">
                                            <label for=""ph");
                WriteLiteral(@"one-number"" class=""required mb-1"">Phone Number</label>
                                            <input type=""text"" id=""phone-number"" placeholder=""Phone Number"" />
                                        </div>
                                    </div>
                                </div>

                                <div class=""single-input-item single-item-button"">
                                    <button class=""btn obrien-button primary-btn"" id=""add-address"">Save Changes</button>
                                </div>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n<!-- my account wrapper end -->\n");
            DefineSection("FooterScripts", async() => {
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "439284a8bafce0d459cad8be5ef8293aa05ee5d731293", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
            }
            );
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