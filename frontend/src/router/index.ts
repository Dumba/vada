import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Members from "../views/Members.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Members',
    component: Members
  },
  {
    path: '/members/:id',
    name: 'Member',
    component: () => import('../views/Member.vue')
  },
  {
    path: '/families',
    name: 'Families',
    component: () => import('../views/Families.vue')
  },
  {
    path: '/families/:id',
    name: 'Family',
    component: () => import('../views/Family.vue')
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
