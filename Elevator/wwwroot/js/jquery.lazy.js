!function (s, C) { "use strict"; var O = s.jQuery || s.Zepto, o = 0, n = !1; function l(A, h, g, t, e) { var m = 0, u = -1, f = -1, b = !1, v = "afterLoad", p = "load", y = "error", z = "img", w = "src", B = "srcset", L = "sizes", T = "background-image"; function r() { var n, i, o, l; b = 1 < s.devicePixelRatio, g = a(g), 0 <= h.delay && setTimeout(function () { c(!0) }, h.delay), (h.delay < 0 || h.combined) && (t.e = (n = h.throttle, i = function (t) { "resize" === t.type && (u = f = -1), c(t.all) }, l = 0, function (t, e) { var r = new Date - l; function a() { l = +new Date, i.call(A, t) } o && clearTimeout(o), n < r || !h.enableThrottle || e ? a() : o = setTimeout(a, n - r) }), t.a = function (t) { t = a(t), g.push.apply(g, t) }, t.g = function () { return g = O(g).filter(function () { return !O(this).data(h.loadedName) }) }, t.f = function (t) { for (var e = 0; e < t.length; e++) { var r = g.filter(function () { return this === t[e] }); r.length && c(!1, r) } }, c(), O(h.appendScroll).on("scroll." + e + " resize." + e, t.e)) } function a(t) { for (var e = h.defaultImage, r = h.placeholder, a = h.imageBase, n = h.srcsetAttribute, i = h.loaderAttribute, o = h._f || {}, l = 0, u = (t = O(t).filter(function () { var t = O(this), e = N(this); return !t.data(h.handledName) && (t.attr(h.attribute) || t.attr(n) || t.attr(i) || o[e] !== C) }).data("plugin_" + h.name, A)).length; l < u; l++) { var f = O(t[l]), c = N(t[l]), s = f.attr(h.imageBaseAttribute) || a; c === z && s && f.attr(n) && f.attr(n, d(f.attr(n), s)), o[c] === C || f.attr(i) || f.attr(i, o[c]), c === z && e && !f.attr(w) ? f.attr(w, e) : c === z || !r || f.css(T) && "none" !== f.css(T) || f.css(T, "url('" + r + "')") } return t } function c(t, e) { if (g.length) { for (var r = e || g, a = !1, n = h.imageBase || "", i = h.srcsetAttribute, o = h.handledName, l = 0; l < r.length; l++)if (t || e || I(r[l])) { var u = O(r[l]), f = N(r[l]), c = u.attr(h.attribute), s = u.attr(h.imageBaseAttribute) || n, d = u.attr(h.loaderAttribute); u.data(o) || h.visibleOnly && !u.is(":visible") || !((c || u.attr(i)) && (f === z && (s + c !== u.attr(w) || u.attr(i) !== u.attr(B)) || f !== z && s + c !== u.css(T)) || d) || (a = !0, u.data(o, !0), D(u, f, s, d)) } a && (g = O(g).filter(function () { return !O(this).data(o) })) } else h.autoDestroy && A.destroy() } function D(e, t, r, a) { ++m; var n = function () { F("onError", e), E(), n = O.noop }; F("beforeLoad", e); var i = h.attribute, o = h.srcsetAttribute, l = h.sizesAttribute, u = h.retinaAttribute, f = h.removeAttribute, c = h.loadedName, s = e.attr(u); if (a) { var d = function () { f && e.removeAttr(h.loaderAttribute), e.data(c, !0), F(v, e), setTimeout(E, 1), d = O.noop }; e.off(y).one(y, n).one(p, d), F(a, e, function (t) { t ? (e.off(p), d()) : (e.off(y), n()) }) || e.trigger(y) } else { var A = O(new Image); A.one(y, n).one(p, function () { e.hide(), t === z ? e.attr(L, A.attr(L)).attr(B, A.attr(B)).attr(w, A.attr(w)) : e.css(T, "url('" + A.attr(w) + "')"), e[h.effect](h.effectTime), f && (e.removeAttr(i + " " + o + " " + u + " " + h.imageBaseAttribute), l !== L && e.removeAttr(l)), e.data(c, !0), F(v, e), A.remove(), E() }); var g = (b && s ? s : e.attr(i)) || ""; A.attr(L, e.attr(l)).attr(B, e.attr(o)).attr(w, g ? r + g : null), A.complete && A.trigger(p) } } function I(t) { var e = t.getBoundingClientRect(), r = h.scrollDirection, a = h.threshold, n = (0 <= f ? f : f = O(s).height()) + a > e.top && -a < e.bottom, i = (0 <= u ? u : u = O(s).width()) + a > e.left && -a < e.right; return "vertical" === r ? n : "horizontal" === r ? i : n && i } function N(t) { return t.tagName.toLowerCase() } function d(t, e) { if (e) { var r = t.split(","); t = ""; for (var a = 0, n = r.length; a < n; a++)t += e + r[a].trim() + (a !== n - 1 ? "," : "") } return t } function E() { --m, g.length || m || F("onFinishedAll") } function F(t, e, r) { return (t = h[t]) && (t.apply(A, [].slice.call(arguments, 1)), 1) } "event" === h.bind || n ? r() : O(s).on(p + "." + e, r) } function f(t, e) { var r = this, a = O.extend({}, r.config, e), n = {}, i = a.name + "-" + ++o; return r.config = function (t, e) { return e === C ? a[t] : (a[t] = e, r) }, r.addItems = function (t) { return n.a && n.a("string" === O.type(t) ? O(t) : t), r }, r.getItems = function () { return n.g ? n.g() : {} }, r.update = function (t) { return n.e && n.e({}, !t), r }, r.force = function (t) { return n.f && n.f("string" === O.type(t) ? O(t) : t), r }, r.loadAll = function () { return n.e && n.e({ all: !0 }, !0), r }, r.destroy = function () { return O(a.appendScroll).off("." + i, n.e), O(s).off("." + i), n = {}, C }, l(r, a, t, n, i), a.chainable ? t : r } O.fn.Lazy = O.fn.lazy = function (t) { return new f(this, t) }, O.Lazy = O.lazy = function (t, e, r) { if (O.isFunction(e) && (r = e, e = []), O.isFunction(r)) { t = O.isArray(t) ? t : [t], e = O.isArray(e) ? e : [e]; for (var a = f.prototype.config, n = a._f || (a._f = {}), i = 0, o = t.length; i < o; i++)a[t[i]] !== C && !O.isFunction(a[t[i]]) || (a[t[i]] = r); for (var l = 0, u = e.length; l < u; l++)n[e[l]] = t[0] } }, f.prototype.config = { name: "lazy", chainable: !0, autoDestroy: !0, bind: "load", threshold: 500, visibleOnly: !1, appendScroll: s, scrollDirection: "both", imageBase: null, defaultImage: "data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==", placeholder: null, delay: -1, combined: !1, attribute: "data-src", srcsetAttribute: "data-srcset", sizesAttribute: "data-sizes", retinaAttribute: "data-retina", loaderAttribute: "data-loader", imageBaseAttribute: "data-imagebase", removeAttribute: !0, handledName: "handled", loadedName: "loaded", effect: "show", effectTime: 0, enableThrottle: !0, throttle: 250, beforeLoad: C, afterLoad: C, onError: C, onFinishedAll: C }, O(s).on("load", function () { n = !0 }) }(window);