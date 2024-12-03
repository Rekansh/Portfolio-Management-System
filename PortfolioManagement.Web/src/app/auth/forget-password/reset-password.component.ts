import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserModel } from 'src/app/pages/account/user/user.model';
import { UserService } from 'src/app/pages/account/user/user.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
    public user: UserModel = new UserModel();
    public activation: string = '';
    
    constructor(private router: Router,
        private userService: UserService,
        private route: ActivatedRoute,
        public activeModal: NgbActiveModal,
        private myToaster: ToastService) {
    }
    
    ngOnInit() {
        this.route.params.subscribe(x => {
            this.activation = x['activation']
        });
    }
    
    public SetPassowrd(isValid: boolean): any {
        this.user.username = this.activation;
        if (isValid && this.user.password == this.user.confirmPassword) {
            this.userService.updatePassord(this.user).subscribe(data => {
                if (data != null && data == 'expired')
                    this.myToaster.info('Reset password link is expired.');
                else if (data != null && data == 'success') {
                    this.myToaster.success('Password reset successfully. Now you can login using new password.');
                    this.activeModal.close('');
                    this.router.navigate(['auth/login']);
                }
                else if (data != null && data == 'fail')
                    this.myToaster.error('User detail invalid so password reset has been fail. you can contact to admin or reclick on reset password link.');
                else
                    this.myToaster.error('Something happen to wrong. you can contact to admin.');
            });
        } else {
            this.myToaster.warning('Please provide required input.');
        }
    }

    showPassword: { [key in 'new' | 'confirm']: string } = {
        new: 'password',
        confirm: 'password'
      };
    
      togglePassword(field: 'new' | 'confirm'): void {
        this.showPassword[field] = this.showPassword[field] === 'password' ? 'text' : 'password';
      }
    
    public OnClosed(): void {
        this.activeModal.close('');
        this.router.navigate([{ outlets: { popup: null } }]);
    }
    
    
    public Login(): void {
        this.activeModal.close('');
        this.router.navigate(['auth/login']);
    }
}
