import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserModel } from 'src/app/pages/account/user/user.model';
import { UserService } from 'src/app/pages/account/user/user.service';
import { ToastService } from 'src/app/services/toast.service';


@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss']
})
export class ForgetPasswordComponent implements OnInit {
    public user: UserModel = new UserModel();
    public CreateUserModel: UserModel = new UserModel();
    public resetEmail: string = '';

    constructor(private router: Router,
        private userService: UserService,
        public activeModal: NgbActiveModal,
        private myToaster: ToastService) {
    }

    ngOnInit() {
    }

    public forgot(isValid: boolean): any {
        if (isValid) {
            this.user.username = this.resetEmail;
            this.userService.ForgetPassword(this.user).subscribe(data => {
                if (data.id) {
                    if (data.email)
                        this.myToaster.success('Password reset link sent successfully on ' + data.email + ' email. Now you can check your email for reset password link.');
                    else
                        this.myToaster.success('Email address is not exist in your profile, so please contact to your account creator.');
                    this.activeModal.close('');
                    this.router.navigate([{ outlets: { popup: ['auth', 'login'] } }]);
                }
                else{
                    this.myToaster.error('Email or username is invalid so password reset has been fail. Please try with correct email or username.');
                }
            });
        }
        else {
            this.myToaster.warning('Provide required fields.');
        }
    }

    public OnClosed(): void {
        this.activeModal.close('');
        this.router.navigate([{ outlets: { popup: null } }]);
    }
}
