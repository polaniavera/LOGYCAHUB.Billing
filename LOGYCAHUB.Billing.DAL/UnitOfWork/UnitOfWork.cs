using LOGYCAHUB.Billing.DAL;
using LOGYCAHUB.Billing.DAL.GenericRepository;
using LOGYCAHUB.Billing.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGYCAHUB.Billing.DAL.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private CABASNET_PPAL_CO_V1_0Entities _contextPpal = null;
        private CABASNET_TRAZABILIDADEntities _contextTrazabilidad = null;
        private GenericRepositoryPpal<EMPRESA> _empresaRepository;
        private GenericRepositoryPpal<CRM_EMPRESA> _crmEmpresaRepository;
        private GenericRepositoryTrazabilidad<AUDIT_MAESTRO> _auditMaestroRepository;
        private GenericRepositoryTrazabilidad<PRICATS_X_EMPRESA> _pricatsEmpresaRepository;
        private GenericRepositoryTrazabilidad<USER_API> _userApiRepository;
        private GenericRepositoryTrazabilidad<TOKENS> _tokenRepository;
        private GenericRepositoryTrazabilidad<MESSAGES> _messagesRepository;
        private PricatsEmpresaCustomRepository _pricatsEmpresaCustomRepository;
       

        public UnitOfWork()
        {
            _contextPpal = new CABASNET_PPAL_CO_V1_0Entities();
            _contextTrazabilidad = new CABASNET_TRAZABILIDADEntities();
        }

        /// <summary>
        /// Get/Set Property for empresa repository.
        /// </summary>
        public GenericRepositoryPpal<EMPRESA> EmpresaRepository
        {
            get
            {
                if (this._empresaRepository == null)
                {
                    this._empresaRepository = new GenericRepositoryPpal<EMPRESA>(_contextPpal);
                }

                return _empresaRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for crmEmpresa repository.
        /// </summary>
        public GenericRepositoryPpal<CRM_EMPRESA> CrmEmpresaRepository
        {
            get
            {
                if (this._crmEmpresaRepository == null)
                {
                    this._crmEmpresaRepository = new GenericRepositoryPpal<CRM_EMPRESA>(_contextPpal);
                }

                return _crmEmpresaRepository;
            }
        }


        // </summary>
        public GenericRepositoryTrazabilidad  <MESSAGES> MessagesRepository
        {
            get
            {
                if (this._messagesRepository == null)
                {
                    this._messagesRepository = new GenericRepositoryTrazabilidad<MESSAGES>(_contextTrazabilidad);
                }

                return _messagesRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for AuditMaestro repository.
        /// </summary>
        public GenericRepositoryTrazabilidad<AUDIT_MAESTRO> AuditMaestroRepository
        {
            get
            {
                if (this._auditMaestroRepository == null)
                {
                    this._auditMaestroRepository = new GenericRepositoryTrazabilidad<AUDIT_MAESTRO>(_contextTrazabilidad);
                }

                return _auditMaestroRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for documento repository.
        /// </summary>
        public GenericRepositoryTrazabilidad<PRICATS_X_EMPRESA> PricatsEmpresaRepository
        {
            get
            {
                if (this._pricatsEmpresaRepository == null)
                {
                    this._pricatsEmpresaRepository = new GenericRepositoryTrazabilidad<PRICATS_X_EMPRESA>(_contextTrazabilidad);
                }

                return _pricatsEmpresaRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepositoryTrazabilidad<USER_API> UserApiRepository
        {
            get
            {
                if (this._userApiRepository == null)
                {
                    this._userApiRepository = new GenericRepositoryTrazabilidad<USER_API>(_contextTrazabilidad);
                }

                return _userApiRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepositoryTrazabilidad<TOKENS> TokenRepository
        {
            get
            {
                if (this._tokenRepository == null)
                {
                    this._tokenRepository = new GenericRepositoryTrazabilidad<TOKENS>(_contextTrazabilidad);
                }

                return _tokenRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for pricatesEmpresa Custom repository.
        /// </summary>
        public PricatsEmpresaCustomRepository PricatsEmpresaCustomRepository
        {
            get
            {
                if (this._pricatsEmpresaCustomRepository == null)
                {
                    this._pricatsEmpresaCustomRepository = new PricatsEmpresaCustomRepository();
                }

                return _pricatsEmpresaCustomRepository;
            }
        }


        #region Public member methods...
        /// <summary>
        /// Save Ppal method.
        /// </summary>
        public void SavePpal()
        {
            try
            {
                _contextPpal.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        /// <summary>
        /// Save Trazabilidad method.
        /// </summary>
        public void SaveTrazabilidad()
        {
            try
            {
                _contextTrazabilidad.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _contextPpal.Dispose();
                    _contextTrazabilidad.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
