﻿import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { AuthLayoutComponent } from '../layouts/auth-layout/auth-layout.component';
import { RegistrationComponent } from './registration/registration.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ProfileComponent } from './profile/profile.component';
import { RegistrationActiveComponent } from './registration/registration-active.component';
import { ResetPasswordComponent } from './forget-password/reset-password.component';

const routes: Routes = [
    {
        path: '',
        component: AuthLayoutComponent,
        children: [
            {
                path: 'login',
                component: LoginComponent,
            },
            {
                path: 'registration',
                component: RegistrationComponent
            },
            {
                path: 'activate/:activation',
                component: RegistrationActiveComponent
            },
            {
                path: 'forgetPassword',
                component: ForgetPasswordComponent
            },
            { 
                path: 'resetPassword/:activation', 
                component:  ResetPasswordComponent
            },
            { 
                path: '', 
                redirectTo: 'login', 
                pathMatch: 'full' 
            }
        ],
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AuthRoute {
}
