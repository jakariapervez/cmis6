(self.webpackChunklite=self.webpackChunklite||[]).push([[8831],{85714:(e,t,n)=>{"use strict";n.d(t,{ZP:()=>d,v:()=>p,QX:()=>m});var r=n(28655),o=n.n(r),i=n(71439),a=n(67294),l=n(12291),s=n(85277);function c(){var e=o()(["\n  fragment SuspendedBannerLoader_post on Post {\n    id\n    isSuspended\n  }\n"]);return c=function(){return e},e}function u(){var e=o()(["\n  fragment SuspendedBannerLoader_user on User {\n    id\n    isSuspended\n  }\n"]);return u=function(){return e},e}const d=(0,l.$j)()((function(e){var t=e.dispatch,n=e.user,r=e.post;return a.useEffect((function(){n&&n.isSuspended?t((0,s.Dx)({duration:"NEXTPAGE",message:"",toastStyle:"USER_SUSPENDED"})):r&&r.isSuspended&&t((0,s.Dx)({duration:"NEXTPAGE",message:"",toastStyle:"POST_SUSPENDED",extraParams:{postId:(null==r?void 0:r.id)||""}}))}),[]),null}));var p=(0,i.Ps)(u()),m=(0,i.Ps)(c())},68831:(e,t,n)=>{"use strict";n.d(t,{gc:()=>Qe,De:()=>$e,m6:()=>Ke});var r=n(28655),o=n.n(r),i=n(67154),a=n.n(i),l=n(63038),s=n.n(l),c=n(71439),u=n(58875),d=n.n(u),p=n(67294),m=n(12291),f=n(28859),v=n(64946),h=n(59713),g=n.n(h),E=n(68254),y=n.n(E);function S(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function b(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?S(Object(n),!0).forEach((function(t){g()(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):S(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}const P=(0,m.$j)((function(e){return{googleAnalyticsCode:e.config.googleAnalyticsCode}}))((function(e){var t=e.googleAnalyticsCode,n=e.collectionGoogleAnalyticsId,r=n?{trackCollectionPageview:{on:"visible",request:"pageview",vars:{account:n}}}:{},o={triggers:b(b({},{trackPageview:{on:"visible",request:"pageview",vars:{account:t}}}),r)},i={__html:'<script type="application/json">'.concat(y()(o,{isJSON:!0}),"<\/script>")};return p.createElement("amp-analytics",{type:"googleanalytics",id:"google-analytics",dangerouslySetInnerHTML:i})}));var w=n(85549),I=n(638),_=n(45113),x=n(9972),C=n(94132),k=n(26463),B=n(62872),D=n(31933),O=n(4995),A=n(86987),M=n(46829),T=n(9785);function L(){var e=o()(["\n  mutation AddCollaboratorMutation($postId: ID!) {\n    addCollaborator(postId: $postId)\n  }\n"]);return L=function(){return e},e}function U(){var e=o()(["\n  fragment AddCollaborator_post on Post {\n    isPublished\n    id\n  }\n"]);return U=function(){return e},e}var j=(0,c.Ps)(U()),R=(0,c.Ps)(L()),F=n(47578),V=n(12390),H=n(66371),N=n(4442),G=n(47921),z=n(53099),Z=n(49925),q=n(59877),W=n(28309),J=n(534);function Q(){var e=o()(["\n  fragment PostScreenThemeProvider_post on Post {\n    collection {\n      colorPalette {\n        ...customDefaultBackgroundTheme_colorPalette\n      }\n      ...auroraHooks_publisher\n    }\n    creator {\n      ...auroraHooks_publisher\n    }\n    customStyleSheet {\n      ...customDefaultBackgroundTheme_customStyleSheet\n      ...customStyleSheetFontTheme_customStyleSheet\n    }\n  }\n  ","\n  ","\n  ","\n  ","\n"]);return Q=function(){return e},e}var X=function(e){var t=e.children,n=e.post,r=(0,Z.GT)(n.collection||n.creator),o=(0,W.Fg)();if(r){var i,a=(0,J.ZI)(n.customStyleSheet,(0,J.zI)(n.customStyleSheet,o,null===(i=n.collection)||void 0===i?void 0:i.colorPalette));return p.createElement(W.f6,{theme:a},t)}var l,s=(0,J.zI)(null,o,null===(l=n.collection)||void 0===l?void 0:l.colorPalette);return p.createElement(W.f6,{theme:s},p.createElement(q.r,null,t))},$=(0,c.Ps)(Q(),Z.C5,J.L9,J.Ui,J.VE),K=n(62182),Y=n(50493),ee=n(15789),te=n(9482),ne=n(90163),re=n(50188),oe=n(47012),ie=n(55127),ae=n(56365),le=function(e){var t=e.children,n=e.rule,r=(0,W.Iq)(),o=n?r(n):"";return o?p.createElement("div",{className:o},t):p.createElement(p.Fragment,null,t)},se=n(85489),ce=n(49514),ue=n(85740),de=n(99308),pe=n(89894),me=n(64504),fe=n(85277),ve=n(67122);function he(){return(he=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e}).apply(this,arguments)}var ge=p.createElement("path",{fill:"#fff",d:"M0 0h49v57H0z"}),Ee=p.createElement("path",{d:"M45.95 17.53v-.04l-.05-1.55a5.32 5.32 0 0 0-5.04-5.17c-5.8-.32-10.3-2.2-14.13-5.89l-.03-.03a3.26 3.26 0 0 0-4.4 0l-.03.03c-3.84 3.7-8.33 5.57-14.13 5.89-2.79.15-5 2.42-5.04 5.18l-.05 1.54v.1c-.11 5.84-.25 13.12 2.21 19.73 1.36 3.63 3.41 6.79 6.1 9.38 3.07 2.96 7.09 5.3 11.94 6.98a3.74 3.74 0 0 0 2.4 0c4.85-1.68 8.86-4.03 11.93-6.98 2.69-2.6 4.74-5.76 6.1-9.39 2.47-6.63 2.33-13.92 2.22-19.78zm-5 18.77c-2.6 6.96-7.9 11.74-16.23 14.62a.67.67 0 0 1-.45 0c-8.32-2.87-13.63-7.65-16.23-14.61-2.27-6.1-2.14-12.78-2.03-18.67v-.03c.03-.5.04-1.04.05-1.62.02-1.22 1-2.23 2.24-2.3 3.29-.18 6.17-.81 8.83-1.92 2.64-1.1 5-2.67 7.19-4.77.1-.1.25-.1.36 0a23.11 23.11 0 0 0 7.2 4.77 26.27 26.27 0 0 0 8.82 1.92 2.36 2.36 0 0 1 2.24 2.3c0 .58.02 1.12.05 1.62.11 5.9.24 12.59-2.04 18.69z",fill:"#757575"}),ye=p.createElement("path",{d:"M24.5 17.76c-7.11 0-12.9 5.4-12.9 12.04 0 6.64 5.79 12.04 12.9 12.04s12.9-5.4 12.9-12.04c0-6.64-5.79-12.04-12.9-12.04zm0 21.25c-5.44 0-9.86-4.13-9.86-9.2 0-5.08 4.42-9.21 9.86-9.21 5.44 0 9.86 4.13 9.86 9.2 0 5.08-4.42 9.2-9.86 9.2z",fill:"#757575"}),Se=p.createElement("path",{d:"M28.08 25.1l-5.64 6.12-1.53-1.66c-.56-.6-1.46-.6-2.02 0-.55.6-.55 1.59 0 2.2l2.54 2.75c.28.3.65.45 1.01.45.37 0 .73-.15 1.01-.45l6.65-7.22c.55-.6.55-1.59 0-2.2-.56-.6-1.47-.6-2.02 0z",fill:"#757575"});const be=function(e){return p.createElement("svg",he({width:49,height:57,viewBox:"0 0 49 57",fill:"none"},e),ge,Ee,ye,Se)};var Pe=(0,m.$j)()((function(e){var t=e.dispatch,n=e.isVisible,r=e.hide,o="limitedStateInterstitialDialogPopover";return p.createElement(pe.Vq,{isVisible:n,hide:r,withCloseButton:!1,customBackgroundColor:"rgba(255, 255, 255, 0.94)"},p.createElement(pe.xu,{display:"flex",justifyContent:"center",paddingTop:"100px"},p.createElement(pe.xu,{height:"550px",width:"900px",background:ve.ix,borderRadius:"4px",display:"flex",flexDirection:"column",justifyContent:"center",textAlign:"center",boxShadow:"0 2px 10px ".concat((0,ve.Uy)(.15)),paddingBottom:"14px",md:{width:"600px",height:"auto",padding:"14px 8px"},sm:{width:"95vw !important",padding:"14px 8px"}},p.createElement(pe.xu,{display:"flex",flexDirection:"column",justifyContent:"center",height:"100%"},p.createElement(pe.xu,{paddingBottom:"24px"},p.createElement(be,null)),p.createElement(pe.xu,{paddingBottom:"24px",paddingRight:"24px",paddingLeft:"24px"},p.createElement(me.X6,{scale:"L"},"The following content was reported as a potential violation of Medium’s rules and is under investigation.")),p.createElement(pe.xu,{padding:"0 0 24px 0"},p.createElement(pe.zx,{buttonStyle:"BRAND",size:"REGULAR",onClick:function(){r(),t((0,fe.Dx)({duration:"FOREVER",message:"",toastStyle:"LIMITED_STATE_BANNER"}))}},"I understand and want to proceed anyway"),p.createElement(pe.Bn,null,(function(e){var t=e.isVisible,n=e.toggle,r=e.hide;return p.createElement(pe.J2,{ariaId:o,isVisible:t,hide:r,display:"block",customZIndex:800,popoverRenderFn:function(){return p.createElement(pe.xu,{padding:"15px 20px",width:"400px"},p.createElement(me.F,{scale:"S"},"Anyone can publish on Medium, provided the content adheres to Medium’s rules and policies. When Medium is made aware of potential violations, the content is manually evaluated and appropriate action taken. Some features may be disabled while the content is under review."))},placement:"top",targetDistance:15},p.createElement(pe.xu,{paddingTop:"10px"},p.createElement(pe.rU,{ariaControls:o,ariaExpanded:t?"true":"false",onClick:n},p.createElement("u",null,p.createElement(me.F,{scale:"S"},"Why am I seeing this?")))))})))),p.createElement(pe.xu,{display:"flex",justifyContent:"center"},p.createElement(me.F,{scale:"M"},"View our"," ",p.createElement(pe.rU,{href:"https://medium.com/policy/medium-rules-30e5502c4eb4",linkStyle:"OBVIOUS",inline:!0},"Rules,")," ",p.createElement(pe.rU,{href:"https://medium.com/policy/medium-terms-of-service-9db0094a1e0f",linkStyle:"OBVIOUS",inline:!0},"Terms of Service")," ","&"," ",p.createElement(pe.rU,{href:"https://medium.com/policy/medium-partner-program-terms-fcfe9cf777b8",linkStyle:"OBVIOUS",inline:!0},"Partner Program Terms"))))))}));function we(){var e=o()(["\n  fragment LimitedStateInterstitial_post on Post {\n    creator {\n      id\n    }\n    isLimitedState\n  }\n"]);return we=function(){return e},e}var Ie=function(e){return function(){return{filter:e?"blur(10px)":"blur(0)"}}};function _e(e){var t=e.post,n=t.creator,r=t.isLimitedState,o=e.viewerId,i=e.children,a=(0,W.Iq)(),l=p.useState(!1),c=s()(l,2),u=c[0],d=c[1];return p.useEffect((function(){d(!0)}),[]),r&&(n&&n.id)!==o?p.createElement(pe.Bn,{initialVisibility:!0},(function(e){var t=e.isVisible,n=e.hide;return p.createElement(p.Fragment,null,p.createElement("div",{className:a(Ie(t))},i),u&&p.createElement(Pe,{isVisible:t,hide:n}))})):i}var xe=(0,c.Ps)(we()),Ce=n(85714),ke=n(10886),Be=n(94937),De=n(93874),Oe=n(22091),Ae=n(62630),Me=n(51512),Te=n(72955),Le=n(14391),Ue=n(33572),je=n(11348),Re=n(31517),Fe=n(172);function Ve(e){var t={__html:'<script type="application/json">'.concat(y()(e.json,{isJSON:!0}),"<\/script>")};return p.createElement("amp-analytics",{id:"medium-analytics",dangerouslySetInnerHTML:t})}function He(){var e={__html:'<script type="application/json">'.concat(y()({vars:{apikey:"medium.com"}},{isJSON:!0}),"<\/script>")};return p.createElement("amp-analytics",{type:"parsely",id:"parsely-amp-analytics",dangerouslySetInnerHTML:e})}var Ne=n(27140),Ge=n(77455),ze=n(65441),Ze=n(27952);function qe(){var e=o()(["\n  fragment PostScreen_post on Post {\n    id\n    canonicalUrl\n    collection {\n      id\n      domain\n      googleAnalyticsId\n      slug\n      ...CollectionMetabar_collection\n      ...MetaHeader_publisher\n      ...auroraHooks_publisher\n    }\n    # please note that the postMeteringOptions are defined in the postHandler file\n    content(postMeteringOptions: $postMeteringOptions) {\n      isLockedPreviewOnly\n      validatedShareKey\n      isCacheableContent\n    }\n    creator {\n      isFollowing\n      viewerIsUser\n      ...SuspendedBannerLoader_user\n      ...MetaHeader_publisher\n      ...UserSubdomainFlow_user\n      ...auroraHooks_publisher\n    }\n    customStyleSheet {\n      id\n      postBody {\n        ...SupportedContainerStyles_styleNode\n      }\n      ...CustomBackgroundWrapper_customStyleSheet\n      ...MetaHeader_customStyleSheet\n    }\n    firstPublishedAt\n    isLocked\n    isPublished\n    isShortform\n    layerCake\n    primaryTopic {\n      name\n      slug\n    }\n    title\n    readCreatorPostsCount\n    ...AddCollaborator_post\n    ...InteractivePostBody_post\n    ...LimitedStateInterstitial_post\n    ...MaybeRedirectToEditor_post\n    ...Metabar_post\n    ...PayWall_post\n    ...PostBodyInserts_post\n    ...PostCacheController_post\n    ...PostFooter_post\n    ...PostHeader_post\n    ...PostMetadata_post\n    ...PostPublishedDialog_prerequisite_post\n    ...PostReadTracker_post\n    ...PostScreenThemeProvider_post\n    ...PostScrollTracker_post\n    ...PostSidebar_post\n    ...RegWall_post\n    ...SuspendedBannerLoader_post\n    ...buildBranchViewData_post\n    ...optimizelyData_post\n    ...TableOfContents_post\n  }\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n  ","\n"]);return qe=function(){return e},e}function We(){var e=o()(["\n  fragment PostScreen_meteringInfo on MeteringInfo {\n    __typename\n    postIds\n    maxUnlockCount\n    unlocksRemaining\n    ...PostHeader_meteringInfo\n    ...RegWall_meteringInfo\n    ...buildBranchViewData_meteringInfo\n  }\n  ","\n  ","\n  ","\n"]);return We=function(){return e},e}var Je=function(){return null};function Qe(e){var t,n,r,o=e.meteringInfo,i=e.post,a=e.viewer,l=i.id,c=i.isPublished,u=i.isLocked,h=i.isShortform,g=i.content,E=i.collection,y=i.canonicalUrl,S=(0,m.I0)(),b=(0,Ae.Av)(),x=(0,m.v9)((function(e){return{authDomain:e.config.authDomain,isAmp:e.config.isAmp,isBot:e.client.isBot,inAppBrowser:e.client.inAppBrowserName,currentHash:e.navigation.currentHash,postPublishedType:e.navigation.postPublishedType,referrer:e.navigation.referrer,referrerSource:e.navigation.referrerSource}}),m.wU),k=x.authDomain,B=x.currentHash,L=x.isAmp,U=x.isBot,j=x.inAppBrowser,q=x.postPublishedType,W=x.referrer,J=x.referrerSource,Q=p.useState(!1),$=s()(Q,2),Y=$[0],oe=$[1],ie=p.useState(i.readingList===Le.sx.READING_LIST_QUEUE||i.readingList===Le.sx.READING_LIST_ARCHIVE),pe=s()(ie,2),me=pe[0],ve=pe[1],he=p.useRef(null),ge=!!a,Ee=E||{},ye=Ee.id,Se=Ee.slug,be=Ee.googleAnalyticsId,Pe=g||{},we=Pe.isLockedPreviewOnly,Ie=Pe.validatedShareKey,xe=Pe.isCacheableContent,Oe=be?[be]:null;(0,w.t)(Oe,U,L),(0,Te.Vj)(he,i),(0,Te.t2)(!!ge,he,i),function(e){var t=(0,T.YC)().value,n=(0,M.useMutation)(R,{variables:{postId:e.id}}),r=s()(n,2),o=r[0],i=r[1].called;p.useEffect((function(){e.isPublished||null==t||!t.id||i||o()}),[t])}(i);var qe=(0,Z.GT)(E||i.creator),We=h&&qe,Qe=(0,Ne.D)(qe),$e=(0,Me.P7)(J||"").dimension,Ke=!("digest.reader"!==(void 0===$e?"":$e)||we||!ge||null!==(t=i.creator)&&void 0!==t&&t.viewerIsUser||null!==(n=i.creator)&&void 0!==n&&n.isFollowing||!((i.readCreatorPostsCount||0)>=3)),Ye=p.useState(Ke),et=s()(Ye,2),tt=et[0],nt=et[1];p.useEffect((function(){nt(Ke)}),[]);var rt=(0,H.Dj)(i,tt),ot=(0,de.q)(),it=ot.loading,at=ot.viewerId;p.useEffect((function(){if(!it&&at){if(c){ge||(i.isLocked?(0,je.yB)():(0,je.B8)());var e=(0,_.RD)({inAppBrowser:j,post:i,meteringInfo:o,referrer:W,referrerSource:J,viewer:a,currentUserId:at});E&&E.domain&&(e.data.$canonical_url=(0,Ze.o2)(k,i.id));var t=a&&a.name?(0,Ue.J)(a.name):void 0;(0,F.KQ)({post:i},t),(0,_.Pu)(e),S((0,Fe.aj)(e)),S((0,Fe.QZ)()),b.event("post.clientViewed",{postId:l,collectionId:ye,collectionSlug:Se,context:1,isFriendLink:!!Ie})}else b.event("post.draftViewed",{postId:l});switch(ge||(0,re.kD)(l),q){case"initial":oe(!0),S((0,fe.DJ)());break;case"repub":S((0,fe.Dx)({duration:3e3,message:"Your changes have been published."})),S((0,fe.DJ)())}return function(){S((0,Fe.Uo)())}}}),[l,c,it,at]);var lt=(0,Ge.K)();if(d().canUseDOM&&lt.get("readmore")){var st=document.getElementById(ze.W);if(st){var ct,ut=window.matchMedia&&(null===(ct=window.matchMedia("(prefers-reduced-motion: reduce)"))||void 0===ct?void 0:ct.matches);setTimeout((function(){var e=Math.max(st.offsetTop-window.innerHeight/2,0);window.scrollY<=e&&window.scrollTo({top:e,behavior:ut?void 0:"smooth"})}))}}Qe!==qe&&S((0,Re.t)(qe));var dt,pt,mt=we&&(xe?p.createElement(D.F,null):ge?p.createElement(O.F,{post:i}):p.createElement(A.Mt,{post:i,meteringInfo:o})),ft=u?{className:"meteredContent"}:{};if(B){var vt=B.split("-");dt=vt[0],pt=vt[1]}return p.createElement(p.Fragment,null,p.createElement(I.u,null),p.createElement(v.a,{post:i,isLoggedIn:ge}),p.createElement(V.P,{post:i},p.createElement(G.Z,{isAmp:L,post:i}),(null==a?void 0:a.id)===(null===(r=i.creator)||void 0===r?void 0:r.id)?p.createElement(Ce.ZP,{post:i,user:i.creator}):p.createElement(ue.bZ,{name:"can_view_suspended_content",placeholder:Je},(function(e){return e?p.createElement(Ce.ZP,{post:i,user:i.creator}):null})),p.createElement(X,{post:i},p.createElement(ae.f,{customStyleSheet:i.customStyleSheet},p.createElement(Xe,{post:i}),p.createElement(ce.k,{post:i},(function(e){var t,n=e.show;return p.createElement(le,{rule:(t=i.customStyleSheet,(0,se.fl)(null==t?void 0:t.postBody))},p.createElement(f.Am,null,p.createElement(te.Q.Provider,{value:{isFirstLoadAndInReadingList:me,setIsFirstLoadAndInReadingList:ve}},p.createElement(ke.Q,null),p.createElement(Be.b,null),p.createElement("article",ft,p.createElement(N.mV,{post:i,meteringInfo:o,showMixtape:!1}),p.createElement(_e,{post:i,viewerId:null==a?void 0:a.id},p.createElement(ee.Z,{activeGrafId:dt,activeNoteId:pt,isAuroraPostPageEnabled:qe,post:i,postBodyInserts:rt,ref:he}))),!we&&!!c&&!We&&p.createElement(K.p3,{post:i,showResponsesSidebar:n}),mt,!we&&p.createElement(ne.ZP,{post:i,showResponsesSidebar:n,showMixtape:!0}))))})),p.createElement(z.yx,{hide:function(){return oe(!1)},isVisible:Y,post:i}),!!c&&p.createElement(C.T,{slimFooter:qe}))),L&&p.createElement(p.Fragment,null,p.createElement(Ve,{json:{requests:{base:(0,Ze.vu)(k),pageview:(0,Ze.CX)(),scrollPing:(0,Ze.y4)()},vars:{postId:l,collectionId:ye,canonicalUrl:y,referrer:W},triggers:{defaultPageview:{on:"visible",request:"pageview"},scrollDepths:{on:"scroll",scrollSpec:{verticalBoundaries:[0,5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100]},request:"scrollPing"}},transport:{beacon:!1,xhrpost:!1,image:!0}}}),p.createElement(P,{collectionGoogleAnalyticsId:be}),p.createElement(He,null))),!!i.creator&&!i.collection&&p.createElement(De.h,{user:i.creator,refreshOnComplete:!0}))}var Xe=function(e){var t,n,r=e.post,o=r.collection,i=r.content,l=o||r.creator,s=(0,Z.GT)(l),c=(0,m.v9)((function(e){return e.config.isAmp})),u=i.bodyModel&&i.bodyModel.paragraphs[0],d=u&&u.type===Le.NJ.IMG&&u.layout===Le.ms.FULL_WIDTH;if(l&&s)return p.createElement(Oe.xu,{marginBottom:d?"0":"36px",sm:{marginBottom:d?"0":"20px"}},p.createElement(oe.Go,{publisher:l,customStyleSheet:r.customStyleSheet,post:r,forceSmall:!0}),p.createElement(ie.z,{headerScale:null===(t=r.customStyleSheet)||void 0===t||null===(n=t.header)||void 0===n?void 0:n.headerScale,post:r,publisher:l}));var f={behavior:B.W.Aggressive,isAmp:c,isFixed:!1,post:r};if(o)return p.createElement(x.NL,a()({},f,{collection:o}));var v=r.layerCake,h=r.primaryTopic,g=v&&(1===v||2===v||3===v),E=g?h:null;return p.createElement(k.kw,a()({},f,{isMonogramOnly:!g,marginBottom:0,marginBottomSm:0,topic:E}))},$e=(0,c.Ps)(We(),N.Xc,A.VP,_.PI),Ke=(0,c.Ps)(qe(),j,x.JE,ae.w,ee.U,xe,V.h,k.v7,oe.aU,oe.Mv,O.P,v.C,H.Cs,ne.F9,N.Z3,G.i,z.KR,Te.WZ,$,Te.kH,K.uI,A.E7,se._3,Ce.QX,Ce.v,De.k,Z.C5,_.ir,F.ne,Y.tA)},27140:(e,t,n)=>{"use strict";n.d(t,{D:()=>o});var r=n(67294);function o(e){var t=r.useRef();return r.useEffect((function(){t.current=e}),[e]),t.current}}}]);
//# sourceMappingURL=https://stats.medium.build/lite/sourcemaps/8831.2f4dede3.chunk.js.map