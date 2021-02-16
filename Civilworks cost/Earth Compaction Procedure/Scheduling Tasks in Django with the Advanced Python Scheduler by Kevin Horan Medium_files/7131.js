(self.webpackChunklite=self.webpackChunklite||[]).push([[7131],{57131:(e,n,t)=>{"use strict";t.d(n,{Z:()=>le,G:()=>ae});var o=t(28655),i=t.n(o),r=t(63038),l=t.n(r),a=t(71439),s=t(67294),c=t(12291),u=t(46829);function d(){var e=i()(["\n  mutation DismissPostMutation($postId: ID!) {\n    dismissPost(postId: $postId)\n  }\n"]);return d=function(){return e},e}var m=(0,a.Ps)(d()),p=t(80439),f=t(85277);function E(){var e=i()(["\n  mutation PostAllowResponsesMutation($targetPostId: ID!, $allowResponses: Boolean!) {\n    setPostAllowResponses(targetPostId: $targetPostId, allowResponses: $allowResponses) {\n      id\n      allowResponses\n    }\n  }\n"]);return E=function(){return e},e}var v=(0,a.Ps)(E()),h=(0,c.$j)()((function(e){var n=e.children,t=e.dispatch,o=e.onCompleted,i=e.targetPostId;return s.createElement(p.mm,{mutation:v,onCompleted:function(){t((0,f.Dx)({message:"Responses are now hidden for this post."})),o&&o()},variables:{targetPostId:i,allowResponses:!1},optimisticResponse:{__typename:"Mutation",setPostAllowResponses:{__typename:"Post",id:i,allowResponses:!1}}},(function(e){return n({mutate:e})}))})),b=(0,c.$j)()((function(e){var n=e.children,t=e.dispatch,o=e.onCompleted,i=e.targetPostId;return s.createElement(p.mm,{mutation:v,onCompleted:function(){t((0,f.Dx)({message:"Responses are now shown for this post."})),o&&o()},variables:{targetPostId:i,allowResponses:!0},optimisticResponse:{__typename:"Mutation",setPostAllowResponses:{__typename:"Post",id:i,allowResponses:!0}}},(function(e){return n({mutate:e})}))})),g=t(28774),C=t(33241),y=t(885),x=t(35848),w=t(42963),P=t(89894),S=function(e){var n=e.onConfirm,t=e.isVisible,o=e.hide;return s.createElement(P.QH,{onConfirm:n,isVisible:t,hide:o,titleText:"Hide responses",confirmText:"Confirm",isDestructiveAction:!1},"People will still be able to respond, but the link to see responses will not be displayed at the bottom of your post.")},I=t(1506),R=t(73882),k=t(71245),_=t(74871),D=t(32262),M=t(64504),U="manageSubmissionPopover",F=function(e){var n=e.viewer,t=e.show,o=e.post;return n&&"PENDING"===o.statusForCollection&&((0,_.DM)(o)||(0,_.py)(o,n))?s.createElement(D.Sl,null,s.createElement(P.rU,{ariaControls:U,ariaExpanded:"false",onClick:t},"Manage submission")):null},A=function(e){var n=e.children,t=e.isVisible,o=e.post,i=e.hide,r=e.showLoadingIndicator,l=(0,k.h3)(o),a=(0,k.yb)(o),u=(0,c.I0)(),d=o.pendingCollection;return d?s.createElement(P.J2,{ariaId:U,isVisible:t,hide:i,popoverRenderFn:function(){return s.createElement(P.xu,{padding:"16px",maxWidth:"280px"},s.createElement(P.xu,{display:"flex",flexDirection:"row",alignItems:"center"},s.createElement(P.xu,{paddingRight:"8px"},s.createElement(R.v,{collection:d,size:40})),s.createElement(M.F,{scale:"S"},"This draft is submitted to ",d.name,".")),s.createElement(P.xu,{flexDirection:"row",display:"flex",paddingTop:"16px"},s.createElement(P.xu,{paddingRight:"8px"},s.createElement(P.zx,{buttonStyle:"OBVIOUS",onClick:function(){i(),r(),l(o.pendingCollection).then((function(){window.location.reload(!1)}))}},"Accept")),s.createElement(P.zx,{buttonStyle:"SUBTLE",onClick:function(){a(o.pendingCollection).then((function(){u((0,f.Dx)({message:"Story removed from ".concat(d.name||"publication")}))})),i()}},"Don't accept")))}},n):n},T="removeFromPublicationPopover",V=function(e){var n=e.viewer,t=e.show,o=e.post;return n&&("APPROVED"===o.statusForCollection&&((0,_.DM)(o)||(0,_.py)(o,n)||(0,_.Hj)(o,n))||"PENDING"===o.statusForCollection&&(0,_.Hj)(o,n))?s.createElement(D.Sl,null,s.createElement(P.rU,{ariaControls:T,ariaExpanded:"false",onClick:t},"Remove story from publication")):null},B=function(e){var n=e.children,t=e.isVisible,o=e.hide,i=e.post,r=e.viewer,l=(0,c.I0)(),a=(0,k.yb)(i),u=i.collection||i.pendingCollection;return u?s.createElement(P.J2,{ariaId:T,isVisible:t,hide:o,popoverRenderFn:function(){return s.createElement(P.xu,{padding:"16px",maxWidth:"280px"},s.createElement(P.xu,{display:"flex",flexDirection:"row",alignItems:"center"},s.createElement(P.xu,{paddingRight:"8px"},s.createElement(R.v,{collection:u,size:40})),"APPROVED"===i.statusForCollection?s.createElement(M.F,{scale:"S"},(0,_.Hj)(i,r)?"Your":"This"," story is published in"," ",u.name,"."):"PENDING"===i.statusForCollection?s.createElement(M.F,{scale:"S"},"Your story is being reviewed by ",u.name," editors."):s.createElement(M.F,{scale:"S"},"This draft is submitted to ",u.name)),s.createElement(P.xu,{flexDirection:"row",display:"flex",paddingTop:"16px"},s.createElement(P.xu,{paddingRight:"8px"},s.createElement(P.zx,{buttonStyle:"SUBTLE",onClick:function(){a(u).then((function(){l((0,f.Dx)({message:"Story removed from ".concat(u.name||"publication")}))}),(function(){l((0,f.Dx)({message:"There was a problem removing the story from ".concat(u.name||"publication"),toastStyle:"ERROR"}))})),o()}},"PENDING"===i.statusForCollection?"Cancel submission":"Remove from ".concat(u.name||"")))))}},n):n},L=t(965),O=t(49925),N=t(78820),H=t(73232),j=t(85740),G=t(9785),Q=t(62181),z=t(18970),K=t(90639),Z=t(14391),$=t(65347),J=t(93394),W=t(76579),q=t(39171),Y=t(51064),X=t(95614),ee=t(96879),ne=t(55573),te=t(27952);function oe(){var e=i()(["\n  fragment CreatorActionOverflowPopover_post on Post {\n    allowResponses\n    id\n    statusForCollection\n    isLocked\n    isPublished\n    clapCount\n    viewerClapCount\n    mediumUrl\n    pinnedAt\n    pinnedByCreatorAt\n    curationEligibleAt\n    mediumUrl\n    responseDistribution\n    shareKey\n    visibility\n    ...useIsPinnedInContext_post\n    pendingCollection {\n      id\n      name\n      viewerIsEditor\n      creator {\n        id\n      }\n      avatar {\n        id\n      }\n      domain\n      slug\n    }\n    creator {\n      id\n      isBlocking\n      ...MutePopoverOptions_creator\n      ...auroraHooks_publisher\n    }\n    collection {\n      id\n      viewerIsEditor\n      name\n      creator {\n        id\n      }\n      avatar {\n        id\n      }\n      domain\n      slug\n      ...MutePopoverOptions_collection\n      ...auroraHooks_publisher\n    }\n    ...ClapMutation_post\n  }\n  ","\n  ","\n  ","\n  ","\n  ","\n"]);return oe=function(){return e},e}var ie=function(e){return{fill:e.baseColor.fill.normal,":hover":{fill:e.baseColor.fill.darker},":focus":{fill:e.baseColor.fill.darker}}};function re(){return s.createElement(P.hS,null,(function(e){return s.createElement(J.Z,{className:e(ie)})}))}var le=function(e){var n,t,o=e.creator,i=e.post,r=e.collection,a=e.showLoadingIndicator,d=e.setIsAuthorOrPubMuted,p=e.setMutedAuthorId,E=e.setMutedPubId,v=e.onPostDismissed,R=e.isMutingFromHomeFeed,k=e.isDismissDisabled,_=s.useState(!1),M=l()(_,2),U=M[0],T=M[1],J=s.useState(!1),oe=l()(J,2),ie=oe[0],le=oe[1],ae=s.useState(!1),se=l()(ae,2),ce=se[0],ue=se[1],de=(0,c.v9)((function(e){return e.multiVote.clapsPerPost})),me=(0,c.I0)(),pe=s.useCallback((function(e){return me((0,$.at)(e))}),[me]),fe=(0,ne.l)(de,i),Ee=fe.clapCount,ve=fe.viewerClapCount,he=(0,f.w)(),be=(0,G.YC)().value,ge=null==be?void 0:be.id,Ce=(0,L.CP)(),ye=function(e){return(0,u.useMutation)(m,{variables:{postId:e}})}(i.id),xe=l()(ye,1)[0],we=(0,N.KQ)(i),Pe=l()(we,2),Se=Pe[0],Ie=Pe[1],Re=(0,O.UL)(i.collection),ke=(0,O.UL)(i.creator),_e=s.useCallback((function(e){var n;he({extraParams:{collectionName:(null===(n=i.collection)||void 0===n?void 0:n.name)||"",becamePinned:!!e.data.setPinnedAt.pinnedAt},message:"",toastStyle:"POST_PINNED_TO_COLLECTION_HOMEPAGE"})}),[null===(n=i.collection)||void 0===n?void 0:n.name]),De=s.useCallback((function(e){he({message:e.data.postSetPinnedByCreatorAt.pinnedByCreatorAt?"This story has been pinned to your profile's homepage":"This story has been unpinned from your profile's homepage"})}),[]),Me=s.useCallback((function(){Ie().then(_e)}),[Ie,_e,null==i||null===(t=i.collection)||void 0===t?void 0:t.name]),Ue=(0,N.In)(i),Fe=l()(Ue,2),Ae=Fe[0],Te=Fe[1],Ve=s.useCallback((function(){Te().then(De)}),[Te,De]),Be=!(0,X.T)(),Le=(0,c.v9)((function(e){return e.config.authDomain})),Oe=(0,Y.O)(!1),Ne=l()(Oe,3),He=Ne[0],je=Ne[1],Ge=Ne[2],Qe=s.useState(!1),ze=l()(Qe,2),Ke=ze[0],Ze=ze[1],$e=i.responseDistribution===Z.Et.DISTRIBUTED,Je=function(){Ze(!0)},We=(0,j.P5)("can_edit_members_only_posts");if(!o||!be)return null;var qe=i.allowResponses,Ye=i.id,Xe=i.pendingCollection,en=i.mediumUrl,nn=i.shareKey,tn=i.visibility,on=(r||{}).viewerIsEditor||(Xe||{}).viewerIsEditor,rn=ge===o.id||on,ln=We&&i&&i.isLocked,an=rn||ln,sn=!!i.curationEligibleAt,cn="UNLISTED"===tn,un="creatorActionOverflowMenu";return Ke?s.createElement(w.Z,{to:(0,te.d0)(Le,i.id)}):s.createElement(P.Bn,null,(function(e){var n=e.isVisible,t=e.toggle,l=e.hide;return s.createElement(P.Bn,null,(function(e){var c=e.isVisible,u=e.show,m=e.hide;return s.createElement(P.Bn,null,(function(e){var f=e.isVisible,w=e.show,_=e.hide;return s.createElement(P.Bn,null,(function(e){var M=e.isVisible,L=e.show,O=e.hide;return s.createElement(g.Z,{targetUserId:null==o?void 0:o.id,viewerId:be.id,onCompleted:m},(function(e){var g=e.mutate;return s.createElement(y.Z,{targetUserId:null==o?void 0:o.id,viewerId:be.id},(function(e){var y=e.mutate;return s.createElement(C.Z,{targetAuthorId:null==o?void 0:o.id,targetPostId:i.id,isBlocking:null==o?void 0:o.isBlocking},(function(e){var C=e.mutate;return s.createElement(b,{targetPostId:Ye},(function(e){var b=e.mutate;return s.createElement(h,{targetPostId:Ye},(function(e){var h=e.mutate;return s.createElement(s.Fragment,null,s.createElement(P.QH,{buttonStyle:"STRONG",cancelText:"Cancel",isVisible:He,onConfirm:Je,hide:Ge,titleText:"Edit story and response",confirmText:"Continue",isDestructiveAction:!1},H.t),s.createElement(P.J2,{ariaId:un,isVisible:n,hide:l,popoverRenderFn:function(){var e;return s.createElement(D.mX,null,s.createElement(s.Fragment,null,r&&on&&Re&&s.createElement(D.Sl,null,s.createElement(P.rU,{onClick:Me},Se?"Unpin this story from ":"Pin this story to ",r.name)),ge&&!k?s.createElement(D.Sl,null,s.createElement(P.rU,{onClick:function(){xe({variables:{postId:i.id}}),v&&v(i.id),l()}},"Dismiss this story")):null,(null===(e=i.creator)||void 0===e?void 0:e.id)===be.id&&ke&&s.createElement(D.Sl,null,s.createElement(P.rU,{onClick:Ve},Ae?"Unpin this story from your profile":"Pin this story to your profile")),Be&&an?s.createElement(s.Fragment,null,s.createElement(D.Sl,null,$e?s.createElement(P.rU,{linkStyle:"SUBTLE",onClick:(0,q.B)(l,je)},"Edit story"):s.createElement(P.rU,{linkStyle:"SUBTLE",href:(0,te.d0)(Le,i.id)},"Edit story")),s.createElement(D.oK,null)):null,rn&&s.createElement(D.Sl,null,s.createElement(P.rU,{href:(0,te.KI)(Le,i.id)},"Story settings")),rn&&s.createElement(D.Sl,null,s.createElement(P.rU,{href:(0,te.T0)(Le,i.id)},"View stats")),sn&&!cn&&en&&nn&&s.createElement(D.Sl,null,s.createElement(W.b,{url:(0,ee.Rk)((0,te.jV)(i),{sk:nn}),linkStyle:"SUBTLE"},(function(e){return e?"Copied!":"Copy Friend Link"}))),s.createElement(x.qT,{hidePopover:l,creator:o,collection:r,postId:i.id,setIsAuthorOrPubMuted:d,setMutedAuthorId:p,setMutedPubId:E,isMutingFromHomeFeed:R}),s.createElement(I.yi,{post:i,show:function(){le(!0),l()}}),s.createElement(V,{viewer:be,post:i,show:function(){ue(!0),l()}}),s.createElement(F,{viewer:be,post:i,show:function(){T(!0),l()}})),ge===o.id||on?s.createElement(s.Fragment,null,s.createElement(D.Sl,null,s.createElement(P.rU,{onClick:function(){qe?L():b(),l()}},qe?"Hide responses":"Show responses")),s.createElement(D.oK,null)):null,ge&&Ee&&ve&&ve>0?s.createElement(D.Sl,null,s.createElement(P.rU,{onClick:function(){Ce(i,(null==be?void 0:be.id)||"",-ve),pe({postId:Ye,clapCount:Ee-ve,viewerClapCount:0,viewerHasClappedSinceFetch:!0}),l()}},"Undo applause for this post")):null,s.createElement(D.Sl,null,ge?s.createElement(P.rU,{onClick:function(){w(),l()}},"Report this story"):s.createElement(Q.R9,{operation:"register",post:i,susiEntry:"report_footer"},"Report this story")),ge?s.createElement(D.Sl,null,s.createElement(P.rU,{onClick:function(){o.isBlocking?y():u(),l()}},o.isBlocking?"Unblock this author":"Block this author")):null)}},s.createElement(I.jB,{post:i,isVisible:ie,hide:function(){le(!1)},showLoadingIndicator:a},s.createElement(B,{viewer:be,post:i,isVisible:ce,hide:function(){ue(!1)}},s.createElement(A,{post:i,isVisible:U,hide:function(){T(!1)},showLoadingIndicator:a},s.createElement(P.xu,{flexGrow:"0"},s.createElement(P.rU,{ariaControls:un,ariaExpanded:n?"true":"false",ariaLabel:"More options",onClick:t},s.createElement(re,null))))))),s.createElement(S,{onConfirm:h,isVisible:M,hide:O}),s.createElement(z.Z,{onConfirm:g,isVisible:c,hide:m}),s.createElement(K.Z,{onSubmit:C,isVisible:f,hide:_}))}))}))}))}))}))}))}))}))}))},ae=(0,a.Ps)(oe(),x.mz,x.Gj,L.JP,N.xt,O.C5)},1506:(e,n,t)=>{"use strict";t.d(n,{x7:()=>_,yi:()=>F,jB:()=>V});var o=t(63038),i=t.n(o),r=t(28655),l=t.n(r),a=t(71439),s=t(80439),c=t(67294),u=t(73882),d=t(71245),m=t(74871),p=t(24087),f=t(32262),E=t(89894),v=t(64504),h=t(72955);function b(){var e=l()(["\n  fragment CollectionSubmission_post on Post {\n    id\n    ...CollectionSubmissionPopover_post\n    ...CollectionSubmissionPopoverMenuItem_post\n  }\n  ","\n  ","\n"]);return b=function(){return e},e}function g(){var e=l()(["\n  fragment CollectionSubmissionPopover_post on Post {\n    id\n    isPublished\n    creator {\n      id\n    }\n  }\n"]);return g=function(){return e},e}function C(){var e=l()(["\n  fragment CollectionSubmissionPopoverMenuItem_post on Post {\n    id\n    statusForCollection\n    isPublished\n    creator {\n      id\n    }\n  }\n"]);return C=function(){return e},e}function y(){var e=l()(["\n  query CollectionSubmissionPopoverQuery {\n    viewer {\n      id\n      writerCollections {\n        ...CollectionSubmissionPopoverQuery_collection\n      }\n      adminCollections {\n        ...CollectionSubmissionPopoverQuery_collection\n      }\n    }\n  }\n  ","\n"]);return y=function(){return e},e}function x(){var e=l()(["\n  fragment CollectionSubmissionPopoverQuery_collection on Collection {\n    ...CollectionSubmissionOption_collection\n    ...ManageSubmission_collection\n  }\n  ","\n  ","\n"]);return x=function(){return e},e}function w(){var e=l()(["\n  fragment CollectionSubmissionOption_collection on Collection {\n    id\n    name\n    slug\n    ...CollectionAvatar_collection\n  }\n  ","\n"]);return w=function(){return e},e}var P=(0,a.Ps)(w(),u.d),S=(0,a.Ps)(x(),P,m.a6),I=(0,a.Ps)(y(),S),R=(0,a.Ps)(C()),k=(0,a.Ps)(g()),_=(0,a.Ps)(b(),k,R),D="collectionSubmissionPopover",M=[{name:"",value:"No publication",content:c.createElement(E.xu,{height:"32px",display:"flex",alignItems:"center",marginLeft:"5px"},c.createElement(v.F,{scale:"M",color:"DARKER"},"No publication"))}];function U(e){var n=e.collection;return n.name?c.createElement(E.xu,{display:"flex",justifyContent:"space-between",width:"200px",alignItems:"center",marginLeft:"5px"},c.createElement(v.F,{scale:"M",color:"DARKER"},n.name),c.createElement(u.v,{collection:n,size:32})):null}function F(e){var n=e.show,t=e.post,o=t.statusForCollection,r=t.isPublished,l=c.useState(!1),a=i()(l,2),s=a[0],u=a[1];return s||h.V6.on("load",(function(){return u(!0)})),!o&&r&&s?c.createElement(T,{post:t,noQueryReturn:null},(function(e){var o=e.loading,i=e.error,r=e.data;if(o||i)return null;var l=(void 0===r?{viewer:void 0}:r).viewer,a=t.creator;if(!l||!a||l.id!==a.id)return null;var s=l.adminCollections,u=l.writerCollections;return 0===s.length&&0===u.length?null:c.createElement(f.Sl,null,c.createElement(E.rU,{ariaControls:D,ariaExpanded:"false",onClick:n},"Add to publication"))})):null}function A(e){var n=e.isVisible,t=e.hide;return c.createElement(E.Vq,{isVisible:n,hide:t,withCloseButton:!0},c.createElement(E.xu,{display:"flex",flexDirection:"column",alignItems:"center",textAlign:"center",maxWidth:"480px"},c.createElement(v.X6,{scale:"M",tag:"h3"},"Story submitted"),c.createElement(E.xu,{paddingTop:"8px",paddingBottom:"24px"},c.createElement(v.QE,{scale:"M"},"We’ll email you when the publication editor has reviewed your submission.")),c.createElement(E.zx,{buttonStyle:"OBVIOUS",onClick:t},"OK")))}var T=function(e){var n=e.children,t=e.post,o=e.noQueryReturn,i=t.creator;return i?c.createElement(p.Z,null,(function(e){return e?e.id!==i.id?o:c.createElement(s.AE,{query:I,ssr:!1},(function(e){var t=e.loading,o=e.error,i=e.data;return n({loading:t,error:o,data:i})})):o})):o},V=function(e){var n=e.post,t=e.hide,o=e.showLoadingIndicator,r=e.isVisible,l=e.children,a=c.useState(!1),s=i()(a,2),u=s[0],m=s[1],p=c.useState(""),f=i()(p,2),b=f[0],g=f[1],C=(0,d.h3)(n),y=(0,d.Lf)(n);return u||h.V6.on("load",(function(){return m(!0)})),n.isPublished&&u?c.createElement(E.Bn,null,(function(e){var i=e.isVisible,a=e.hide,s=e.show;return c.createElement(c.Fragment,null,c.createElement(A,{hide:a,isVisible:i}),c.createElement(T,{post:n,noQueryReturn:l},(function(e){var i=e.loading,a=e.error,u=e.data;if(i||a)return l;var d=(void 0===u?{viewer:void 0}:u).viewer,m=n.creator;if(!d||!m||d.id!==m.id)return l;var p=d.adminCollections,f=d.writerCollections;if(0===p.length&&0===f.length)return l;var h=p.concat(f).filter((function(e){return!!e&&!!e.slug&&!!e.name})).map((function(e){return{name:e.slug||"",value:e.name||"",content:c.createElement(U,{collection:e})}})),x=M.concat(h),w=p.reduce((function(e,n){return e[n.slug||""]=n,e}),{}),P=f.reduce((function(e,n){return e[n.slug||""]=n,e}),{}),S="";if(b){var I=x.find((function(e){return e.name===b}));I&&(S=I.value)}return c.createElement(E.J2,{ariaId:D,placement:"top",hide:t,isVisible:r,popoverRenderFn:function(){return c.createElement(E.xu,{padding:"20px",width:"280px",display:"flex",flexDirection:"column"},c.createElement(v.F,{scale:"M",color:"DARKER"},"Add your story to a publication:"),c.createElement(E.xu,{marginTop:"10px"},c.createElement(E.Ee,{value:S,onChange:g,options:x})),c.createElement(E.xu,{display:"flex",marginTop:"20px"},c.createElement(v.F,{scale:"M",color:"LIGHTER"},b in w?c.createElement(E.zx,{onClick:function(){var e=w[b];t(),o(),C(e).then((function(){window.location.reload(!1)}))},buttonStyle:"OBVIOUS"},"Add story"):c.createElement(E.zx,{onClick:function(){var e=P[b];y(e),t(),s()},disabled:!b},"Submit")),c.createElement(E.xu,{marginLeft:"10px"},c.createElement(v.F,{scale:"M",color:"LIGHTER"},c.createElement(E.rU,{onClick:t},c.createElement(E.xu,{padding:"10px"},"Cancel"))))),c.createElement(E.xu,{marginTop:"10px"},c.createElement(v.F,{scale:"M"},"This story will be automatically submitted to the publication. When you submit your story, the publication's editors will be notified and will be able to edit your story.")))}},l)})))})):l}},74871:(e,n,t)=>{"use strict";t.d(n,{Hj:()=>c,py:()=>u,DM:()=>d,a6:()=>m,En:()=>p});var o=t(28655),i=t.n(o),r=t(71439),l=t(73882);function a(){var e=i()(["\n  fragment ManageSubmission_post on Post {\n    id\n    statusForCollection\n    collection {\n      ...ManageSubmission_collection\n    }\n    pendingCollection {\n      ...ManageSubmission_collection\n    }\n    creator {\n      id\n    }\n  }\n  ","\n"]);return a=function(){return e},e}function s(){var e=i()(["\n  fragment ManageSubmission_collection on Collection {\n    name\n    viewerIsEditor\n    creator {\n      id\n    }\n    ...CollectionAvatar_collection\n  }\n  ","\n"]);return s=function(){return e},e}var c=function(e,n){return n&&e.creator&&n.id===e.creator.id},u=function(e,n){var t=e.pendingCollection||e.collection;return t&&t.creator&&n.id===t.creator.id},d=function(e){var n=e.pendingCollection||e.collection;return n&&n.viewerIsEditor},m=(0,r.Ps)(s(),l.d),p=(0,r.Ps)(a(),m)}}]);
//# sourceMappingURL=https://stats.medium.build/lite/sourcemaps/7131.3e13d480.chunk.js.map