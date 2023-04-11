import React from 'react';
import { Login } from './Login'
import { Logout } from './Logout'
import { ApplicationPaths, LoginActions, LogoutActions } from './ApiAuthorizationConstants';

const ApiAuthorizationRoutes = [
  {
      path: ApplicationPaths.Login,
      element: loginAction(LoginActions.Login),
      requireAuth: false,
  },
  {
      path: ApplicationPaths.LoginFailed,
      element: loginAction(LoginActions.LoginFailed),
      requireAuth: false,
  },
  {
      spath: ApplicationPaths.LoginCallback,
      element: loginAction(LoginActions.LoginCallback),
      requireAuth: false,
  },
  {
      path: ApplicationPaths.Profile,
      element: loginAction(LoginActions.Profile),
      requireAuth: true,
  },
  {
      path: ApplicationPaths.Register,
      element: loginAction(LoginActions.Register),
      requireAuth: false,
  },
  {
      path: ApplicationPaths.LogOut,
      element: logoutAction(LogoutActions.Logout),
      requireAuth: false,
  },
  {
      path: ApplicationPaths.LogOutCallback,
      element: logoutAction(LogoutActions.LogoutCallback),
      requireAuth: false,
  },
  {
      path: ApplicationPaths.LoggedOut,
      element: logoutAction(LogoutActions.LoggedOut),
      requireAuth: false,
  }
];

function loginAction(name){
  return <Login action={name}></Login>;
}

function logoutAction(name) {
  return <Logout action={name}></Logout>;
}

export default ApiAuthorizationRoutes;
