import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import { useAuthStore } from '@/store/authStore'
import LoginView from '@/views/LoginView'

Vue.use(VueRouter)

export let router = null

export function createRouter (pinia) {
  const routes = [
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
    },
    {
      path: '/profile',
      name: 'profile',
      component: () => import(/* webpackChunkName: "about" */ '../views/ProfileView.vue')
    }
  ]

  router = new VueRouter({
    routes,
  })
  router.afterEach((to, from) => {
    if (to.path === '/login') {
      let auth = useAuthStore(pinia)
      auth.destroyUser()
    }
  })
  router.beforeEach(async (to, from, next) => {
    const allowAnon = ['/login']
    let auth = useAuthStore(pinia)
    if (to.path === '/logout'){
      auth.deleteToken();
      await next({ name: 'login' })
    }
    else if (!auth.isAuth && !allowAnon.includes(to.path)) {
      auth.returnUrl = to.path
      console.log(`user not logged in, redirecting from '` + to.path + `'to /login`)
      next({ name: 'login' })
    } else if (auth.isAuth && to.path === '/login') {
      if (from == null) {
        next({ path : '/' })
      }
      next(from)
    } else {
      next()
    }

  })

  return router
}
