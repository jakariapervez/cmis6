(self.webpackChunklite=self.webpackChunklite||[]).push([[9274],{71254:(e,t,n)=>{"use strict";n.d(t,{Z:()=>i});var r=n(67294);function o(){return(o=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e}).apply(this,arguments)}var a=r.createElement("path",{d:"M19 6a2 2 0 0 0-2-2H8a2 2 0 0 0-2 2v14.66h.01c.01.1.05.2.12.28a.5.5 0 0 0 .7.03l5.67-4.12 5.66 4.13a.5.5 0 0 0 .71-.03.5.5 0 0 0 .12-.29H19V6zm-6.84 9.97L7 19.64V6a1 1 0 0 1 1-1h9a1 1 0 0 1 1 1v13.64l-5.16-3.67a.49.49 0 0 0-.68 0z",fillRule:"evenodd"});const i=function(e){return r.createElement("svg",o({width:25,height:25,viewBox:"0 0 25 25"},e),a)}},6106:(e,t,n)=>{"use strict";n.d(t,{Z:()=>i});var r=n(67294);function o(){return(o=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e}).apply(this,arguments)}var a=r.createElement("path",{d:"M19 6a2 2 0 0 0-2-2H8a2 2 0 0 0-2 2v14.66h.01c.01.1.05.2.12.28a.5.5 0 0 0 .7.03l5.67-4.12 5.66 4.13c.2.18.52.17.71-.03a.5.5 0 0 0 .12-.29H19V6z"});const i=function(e){return r.createElement("svg",o({width:25,height:25,viewBox:"0 0 25 25"},e),a)}},86753:(e,t,n)=>{"use strict";n.d(t,{z:()=>x,Z:()=>R});var r=n(63038),o=n.n(r),a=n(28655),i=n.n(a),s=n(71439),l=n(67294),c=n(3021),u=n(24087),p=n(62181),d=n(267),f=n(89894),m=n(62630),v=n(27572),E=n(28309),g=n(14391),I=n(71254),h=n(6106);function b(){return(b=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e}).apply(this,arguments)}var _=l.createElement("path",{d:"M19 6a2 2 0 0 0-2-2H8a2 2 0 0 0-2 2v14.66h.01c.01.1.05.2.12.28a.5.5 0 0 0 .7.03l5.67-4.12 5.66 4.13c.2.18.52.17.71-.03a.5.5 0 0 0 .12-.29H19V6z"});const P=function(e){return l.createElement("svg",b({width:25,height:25,viewBox:"0 0 25 25"},e),_)};var y=n(27952);function C(){var e=i()(["\n  fragment BookmarkButton_post on Post {\n    ...SusiClickable_post\n    ...WithSetReadingList_post\n  }\n  ","\n  ","\n"]);return C=function(){return e},e}var x=(0,s.Ps)(C(),p.qU,c.jy),O=function(e){return{fill:e.baseColor.fill.light}},w=function(e){return{fill:e.baseColor.border.light,cursor:"default"}},D=function(){var e=(0,E.Iq)();return l.createElement(P,{className:e(w)})};function R(e){var t=e.post,n=e.withTooltip,r=void 0===n||n,a=e.susiEntry,i=t.id,s=t.readingList,b=(0,E.Iq)(),_=(0,m.Av)(),P=(0,v.pK)(),C=(0,d.XC)(),x=null==C?void 0:C.READING_LIST_UPDATED,w=l.useState(r),R=o()(w,2),N=R[0],L=R[1],S=l.useState(s||g.sx.READING_LIST_NONE),k=o()(S,2),A=k[0],j=k[1];l.useEffect((function(){j(s||g.sx.READING_LIST_NONE)}),[s]);var T=l.useCallback((function(){r&&L(!0)}),[r]);return l.createElement(u.Z,null,(function(e){return e?s?l.createElement("div",{className:b(O)},l.createElement(c.sN,{post:t,viewer:e},(function(e){return l.createElement(f.$W,{isVisible:N,darkTheme:!0,placement:"top",mouseLeaveDelay:0,targetDistance:10,popoverRenderFn:function(){return l.createElement(f.xu,{padding:"8px"},{READING_LIST_NONE:"Save story",READING_LIST_ARCHIVE:"Archived",READING_LIST_QUEUE:"Unsave Story"}[s])},onMouseLeave:T},l.createElement(f.rU,{onClick:function(){return function(e){if(s){var n=(o=s,{READING_LIST_NONE:g.sx.READING_LIST_QUEUE,READING_LIST_ARCHIVE:null,READING_LIST_QUEUE:g.sx.READING_LIST_NONE}[o]);if(!n)return;j(n),_.event(function(e){return{READING_LIST_NONE:"post.addedBookmark",READING_LIST_ARCHIVE:"post.addedArchive",READING_LIST_QUEUE:"post.removedBookmark"}[e]}(s),{postId:i,source:P}),e(n)().catch((function(){j(s)})),r&&L(!1),x&&x(t,n)}var o}(e)},ariaLabel:"Bookmark Post"},(n=A,{READING_LIST_NONE:l.createElement(I.Z,null),READING_LIST_ARCHIVE:l.createElement(D,null),READING_LIST_QUEUE:l.createElement(h.Z,null)}[n])));var n}))):null:l.createElement(f.$W,{isVisible:N,darkTheme:!0,placement:"top",mouseLeaveDelay:0,targetDistance:10,popoverRenderFn:function(){return l.createElement(f.xu,{padding:"8px"},"Bookmark story")}},l.createElement(p.R9,{post:t,operation:"register",actionUrl:(0,y.XE)(i),susiEntry:a},l.createElement(I.Z,{className:b(O)})))}))}},3021:(e,t,n)=>{"use strict";n.d(t,{jy:()=>_,sN:()=>P});var r=n(63038),o=n.n(r),a=n(28655),i=n.n(a),s=n(71439),l=n(46829),c=n(14391);function u(){var e=i()(["\n  fragment WithSetReadingList_post on Post {\n    ...ReadingList_post\n  }\n  ","\n"]);return u=function(){return e},e}function p(){var e=i()(["\n  mutation UnarchivePostDefault($targetPostId: ID!) {\n    unarchivePost(targetPostId: $targetPostId) {\n      ...ReadingList_post\n    }\n  }\n  ","\n"]);return p=function(){return e},e}function d(){var e=i()(["\n  mutation ArchivePostDefault($targetPostId: ID!) {\n    archivePost(targetPostId: $targetPostId) {\n      ...ReadingList_post\n    }\n  }\n  ","\n"]);return d=function(){return e},e}function f(){var e=i()(["\n  mutation UnbookmarkPostDefault($targetPostId: ID!) {\n    unbookmarkPost(targetPostId: $targetPostId) {\n      ...ReadingList_post\n    }\n  }\n  ","\n"]);return f=function(){return e},e}function m(){var e=i()(["\n  mutation BookmarkPostDefault($targetPostId: ID!) {\n    bookmarkPost(targetPostId: $targetPostId) {\n      ...ReadingList_post\n    }\n  }\n  ","\n"]);return m=function(){return e},e}function v(){var e=i()(["\n  fragment ReadingList_post on Post {\n    __typename\n    id\n    readingList\n  }\n"]);return v=function(){return e},e}var E=(0,s.Ps)(v()),g=(0,s.Ps)(m(),E),I=(0,s.Ps)(f(),E),h=(0,s.Ps)(d(),E),b=(0,s.Ps)(p(),E),_=(0,s.Ps)(u(),E),P=function(e){var t=e.children,n=e.post,r=n.id,a=function(e){var t=(0,l.useMutation)(g,{variables:{targetPostId:e},optimisticResponse:{bookmarkPost:{__typename:"Post",id:e,readingList:c.sx.READING_LIST_QUEUE}}});return o()(t,1)[0]}(r),i=function(e){var t=(0,l.useMutation)(I,{variables:{targetPostId:e},optimisticResponse:{unbookmarkPost:{__typename:"Post",id:e,readingList:c.sx.READING_LIST_NONE}}});return o()(t,1)[0]}(r),s=function(e){var t=(0,l.useMutation)(h,{variables:{targetPostId:e},optimisticResponse:{archivePost:{__typename:"Post",id:e,readingList:c.sx.READING_LIST_ARCHIVE}}});return o()(t,1)[0]}(r),u=function(e){var t=(0,l.useMutation)(b,{variables:{targetPostId:e},optimisticResponse:{unarchivePost:{__typename:"Post",id:e,readingList:c.sx.READING_LIST_QUEUE}}});return o()(t,1)[0]}(r);return r?t((function(e){return function(){switch(n.readingList){case c.sx.READING_LIST_NONE:if("READING_LIST_QUEUE"===e)return a();break;case c.sx.READING_LIST_QUEUE:if("READING_LIST_NONE"===e)return i();if("READING_LIST_ARCHIVE"===e)return s();break;case c.sx.READING_LIST_ARCHIVE:if(!e)return r;if("READING_LIST_NONE"===e)return i();if("READING_LIST_QUEUE"===e)return u()}throw new Error('Invalid reading list change from "'.concat(n.readingList||"unkown",'" to "').concat(e,'".'))}})):null}},965:(e,t,n)=>{"use strict";n.d(t,{JP:()=>g,CP:()=>h});var r=n(59713),o=n.n(r),a=n(63038),i=n.n(a),s=n(28655),l=n.n(s),c=n(71439),u=n(46829),p=n(67294),d=n(25665);function f(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function m(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?f(Object(n),!0).forEach((function(t){o()(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):f(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function v(){var e=l()(["\n  mutation ClapMutation($targetPostId: ID!, $userId: ID!, $numClaps: Int!) {\n    clap(targetPostId: $targetPostId, userId: $userId, numClaps: $numClaps) {\n      ...ClapMutation_post\n    }\n  }\n  ","\n"]);return v=function(){return e},e}function E(){var e=l()(["\n  fragment ClapMutation_post on Post {\n    __typename\n    id\n    clapCount\n    viewerClapCount\n    ...MultiVoteCount_post\n  }\n  ","\n"]);return E=function(){return e},e}var g=(0,c.Ps)(E(),d.U),I=(0,c.Ps)(v(),g),h=function(){var e=(0,u.useMutation)(I),t=i()(e,1)[0];return(0,p.useCallback)((function(e,n,r){return t({variables:{targetPostId:e.id,userId:n,numClaps:r},optimisticResponse:{clap:m(m({__typename:"Post"},e),{},{clapCount:e.clapCount+r,viewerClapCount:(e.viewerClapCount||0)+r})},update:function(t,n){var o,a=null==n||null===(o=n.data)||void 0===o?void 0:o.clap;if(a){var i=t.readFragment({id:"Post:".concat(a.id),fragment:g,fragmentName:"ClapMutation_post"});t.writeFragment({id:"Post:".concat(a.id),fragment:g,fragmentName:"ClapMutation_post",data:m(m({},i),{},{clapCount:e.clapCount+r,viewerClapCount:(e.viewerClapCount||0)+r})})}}})}),[])}},25665:(e,t,n)=>{"use strict";n.d(t,{_:()=>ne,U:()=>re});var r=n(28655),o=n.n(r),a=n(59713),i=n.n(a),s=n(71439),l=n(23450),c=n.n(l),u=n(67294),p=n(67154),d=n.n(p),f=n(319),m=n.n(f),v=n(6479),E=n.n(v),g=n(87757),I=n.n(g),h=n(48926),b=n.n(h),_=n(63038),P=n.n(_),y=n(82492),C=n.n(y),x=n(80439),O=n(12291),w=n(98281),D=n(31001),R=n(89894),N=n(64504),L=n(67995);function S(){return(S=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e}).apply(this,arguments)}var k=u.createElement("g",{fillRule:"evenodd"},u.createElement("path",{d:"M7.94 1h-.89L7.5 2.9zM10.09 1.33l-.84-.3-.23 1.95zM5.73 1.04l-.84.3L5.97 3zM5.63 11.57a3043.52 3043.52 0 0 0-1.6-1.6C2.32 8.26 1.25 7.5 1.75 7c.25-.25.62-.3.93 0 .45.46 1.54 1.65 1.54 1.65a.69.69 0 0 0 .34.2c.17.04.36-.06.5-.2.14-.13.06-.47-.06-.6L2.94 5.98c-.29-.29-.39-.78-.08-1.09.3-.29.64-.14.9.12l2.1 2.15a.33.33 0 0 0 .24.1.42.42 0 0 0 .26-.12c.13-.12.2-.36.07-.49L5 5.2c-.56-.56-.6-.95-.36-1.2.35-.35.82-.24 1.45.48l2.8 2.95-.59-1.46s-.37-.97 0-1.17c.37-.2.74.33 1 .72l1.37 2.62a3.29 3.29 0 0 1-.57 4.05c-1.22 1.22-3.18.69-4.48-.6z"}),u.createElement("path",{d:"M11.37 4.73c-.26-.4-.7-.4-.98-.19-.19.15-.16.48-.15.7l1.18 2.07c.91 1.49 1.23 2.7.19 4.1.31-.14.4-.27.58-.49.65-.8 1.05-2.47.39-3.88a3.35 3.35 0 0 0-.03-.05l-1.18-2.26z"}));const A=function(e){return u.createElement("svg",S({width:15,height:15},e),k)};var j=n(27952);function T(){var e=o()(["\n  query PostVotersDialogQuery($postId: ID!, $pagingOptions: PagingOptions) {\n    post(id: $postId) {\n      id\n      title\n      clapCount\n      voterCount\n      voters(paging: $pagingOptions) {\n        items {\n          user {\n            id\n            name\n            bio\n            username\n            ...UserAvatar_user\n            ...UserFollowButton_user\n            ...userUrl_user\n          }\n          clapCount\n        }\n        pagingInfo {\n          next {\n            page\n          }\n        }\n      }\n      ...UserFollowButton_post\n    }\n  }\n  ","\n  ","\n  ","\n  ","\n"]);return T=function(){return e},e}var U=function(e){return{position:"relative",bottom:"12px",borderRadius:"10px",color:e.backgroundColor,fill:e.backgroundColor,background:e.accentColor.fill.normal,textAlign:"center"}},V={left:"20px",padding:"0 6px"},G={left:"24px",padding:"0 2px"},M=function(e){var t=e.clapCount,n=(0,L.n)({name:"detail",color:"DARKER",scale:"S"}),r=t?[n,U,V]:[U,G];return u.createElement(R.hS,null,(function(e){return u.createElement("span",{className:e(r)},t?"+".concat(t):u.createElement(A,null))}))},$=function(e){var t=e.user,n=e.clapCount,r=e.post,o=(0,O.v9)((function(e){return e.config.authDomain})),a=t.username,i=t.name,s=t.bio,l=void 0===s?"":s;return i&&a?u.createElement(R.xu,{padding:"12px 0"},u.createElement(R.xu,{display:"flex",alignItems:"flex-start",justifyContent:"space-between"},u.createElement(R.xu,{display:"flex",alignItems:"flex-start"},u.createElement(R.xu,{marginRight:"20px"},u.createElement(w.ZP,{user:t,scale:"S"}),u.createElement(M,{clapCount:n})),u.createElement(R.xu,{display:"flex",flexDirection:"column",alignItems:"flex-start"},u.createElement(R.rU,{href:(0,j.AW)(t,o)},u.createElement(N.X6,{scale:"XS"},i," ")),u.createElement(N.F,{scale:"S"},l))),u.createElement(R.xu,{marginLeft:"48px"},u.createElement(D.Bv,{buttonSize:"SMALL",post:r,user:t,susiEntry:"follow_list"})))):null},H=function(e){var t=e.isVisible,n=e.hide,r=e.post,o=e.fetchMore,a=u.useState(!1),i=P()(a,2),s=i[0],l=i[1],c=u.useCallback(b()(I().mark((function e(){return I().wrap((function(e){for(;;)switch(e.prev=e.next){case 0:if(!o||s){e.next=8;break}return l(!0),e.prev=2,e.next=5,o();case 5:return e.prev=5,l(!1),e.finish(5);case 8:case"end":return e.stop()}}),e,null,[[2,,5,8]])}))),[o,s,l]);if(!r)return null;var p=r.title,d=r.voters,f=r.clapCount,m=r.voterCount;return u.createElement(R.Vq,{isVisible:t,hide:n,withAnimation:!0},u.createElement(R.xu,{maxWidth:"550px",sm:{paddingTop:"0"},paddingTop:"88px"},u.createElement(R.xu,{display:"flex",flexDirection:"column",marginBottom:"24px",textAlign:"center"},u.createElement(N.X6,{scale:"S"},f," claps from ",m," ",1===m?"person":"people",' for "',p,'"')),d&&d.items.map((function(e){var t=e.user,n=e.clapCount;return t&&u.createElement($,{user:t,clapCount:n,post:r,key:t.id})})),o&&u.createElement(R.xu,{display:"flex",flexDirection:"column",margin:"24px",alignItems:"center"},u.createElement(R.zx,{buttonStyle:"SOCIAL",size:"SMALL",onClick:c},"Show more claps"))))};function F(e){var t=e.postId,n=e.isVisible,r=E()(e,["postId","isVisible"]);return n?u.createElement(x.AE,{ssr:!1,query:B,variables:{postId:t,pagingOptions:{limit:10}}},(function(e){var t,o=e.data,a=(o=void 0===o?{}:o).post,i=e.loading,s=e.error,l=e.fetchMore;if(i)return u.createElement(R.TF,null);if(s||null==a||!a.voters)return null;var c=a.voters.pagingInfo&&a.voters.pagingInfo.next;if(c){var p={page:c.page};t=function(){return l({variables:{pagingOptions:p},updateQuery:function(e,t){var n,r,o,a,i,s,l=t.fetchMoreResult;return C()({},l,{post:{voters:{items:[].concat(m()(null!==(n=null==e||null===(r=e.post)||void 0===r||null===(o=r.voters)||void 0===o?void 0:o.items)&&void 0!==n?n:[]),m()(null!==(a=null==l||null===(i=l.post)||void 0===i||null===(s=i.voters)||void 0===s?void 0:s.items)&&void 0!==a?a:[]))}}})}})}}return u.createElement(H,d()({isVisible:n,post:a,loading:i,fetchMore:t},r))})):null}var B=(0,s.Ps)(T(),w.WQ,D.sj,D.S$,j.$m),z=n(27390);function Q(){var e=o()(["\n  fragment PostVotersNetwork_post on Post {\n    voterCount\n    viewerClapCount\n    recommenders {\n      name\n    }\n  }\n"]);return Q=function(){return e},e}var Z=function(e){var t=e.post,n=e.showVoters,r=(t.viewerClapCount?[{name:"you"}]:[]).concat(t.recommenders||[]).slice(0,2).map((function(e){return e.name}));if(!r.length)return null;var o=(t.voterCount||0)-r.length,a=r.join(o>0?", ":" and "),i="".concat(r.length>1?",":""," and"),s=c()("other",o),l=o>0?"".concat(i," ").concat((0,z.rR)(o)," ").concat(s):"";return u.createElement(R.xu,{sm:{display:"none"}},u.createElement(R.rU,{onClick:n},u.createElement(N.F,{scale:"S"},"Applause from ".concat(a).concat(l))))},q=(0,s.Ps)(Q()),W=n(71542),X=n(19464),K=n(98024),J=n(28309);function Y(){var e=o()(["\n  fragment MultiVoteCount_post on Post {\n    id\n    ...PostVotersNetwork_post\n  }\n  ","\n"]);return Y=function(){return e},e}function ee(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function te(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?ee(Object(n),!0).forEach((function(t){i()(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):ee(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function ne(e){var t=e.clapCount,n=e.hasLabel,r=void 0!==n&&n,o=e.showFullNumber,a=void 0!==o&&o,i=e.post,s=e.shouldShowNetwork,l=e.hasDialog,p=void 0!==l&&l,d=e.shouldShowResponsiveLabelText,f=void 0!==d&&d,m=e.shouldHideClapsText,v=void 0!==m&&m,E=i.id;if(!(t>0))return null;var g=r&&!v?c()("clap",t):"",I=function(e){var t=e.showVoters;return s&&t?u.createElement(Z,{showVoters:t,post:i}):null},h=a?(0,z.rR)(t):(0,z.pY)(t),b=function(e){var t=e.showVoters,n=(0,J.Iq)();return u.createElement("div",{className:n(te(te({},f||r?{position:"relative",top:"1px"}:{}),{},{"& button":{textAlign:"left"}}))},u.createElement(K.F,{color:f||r||v?"DARKER":"LIGHTER",scale:"M"},f?u.createElement(u.Fragment,null,t?u.createElement(R.rU,{onClick:t},h,u.createElement(W.s,null," ",g)):h,u.createElement(W.s,null,u.createElement(I,{showVoters:t}))):u.createElement(u.Fragment,null,t?u.createElement(R.rU,{onClick:t},h," ",g):h,u.createElement(I,{showVoters:t}))))};return p&&E?u.createElement(X.Z,null,(function(e){var t=e.isVisible,n=e.show,r=e.hide;return u.createElement(u.Fragment,null,u.createElement(b,{showVoters:n}),u.createElement(F,{isVisible:t,hide:r,postId:E}))})):u.createElement(b,null)}var re=(0,s.Ps)(Y(),q)},71542:(e,t,n)=>{"use strict";n.d(t,{s:()=>c,e:()=>u});var r=n(67154),o=n.n(r),a=n(6479),i=n.n(a),s=n(67294),l=n(89894),c=function(e){var t=e.xs,n=e.sm,r=e.children,a=i()(e,["xs","sm","children"]);return s.createElement(l.xu,o()({xs:{display:"none"},sm:{display:t?"inline-block":"none"},md:{display:t||n?"inline-block":"none"},lg:{display:"inline-block"},xl:{display:"inline-block"},tag:"span"},a),r)},u=function(e){var t=e.xs,n=e.sm,r=e.children,a=i()(e,["xs","sm","children"]);return s.createElement(l.xu,o()({xs:{display:"inline-block"},sm:{display:t?"none":"inline-block"},md:{display:t||n?"none":"inline-block"},lg:{display:"none"},xl:{display:"none"},tag:"span"},a),r)}},267:(e,t,n)=>{"use strict";n.d(t,{XC:()=>c,EI:()=>u});var r=n(59713),o=n.n(r),a=n(67294);function i(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(e);t&&(r=r.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,r)}return n}function s(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?i(Object(n),!0).forEach((function(t){o()(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):i(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}var l=a.createContext({}),c=function(){return a.useContext(l)},u=function(e){var t=e.context,n=e.extendContext,r=void 0!==n&&n,o=e.children,i=c();return r&&i&&(t=s(s({},i),t)),a.createElement(l.Provider,{value:t},o)}},55573:(e,t,n)=>{"use strict";function r(e,t){return!!t&&e[t.id]||{clapCount:(null==t?void 0:t.clapCount)||0,viewerClapCount:(null==t?void 0:t.viewerClapCount)||0,viewerHasClappedSinceFetch:!1}}n.d(t,{l:()=>r})}}]);
//# sourceMappingURL=https://stats.medium.build/lite/sourcemaps/9274.5526dd29.chunk.js.map