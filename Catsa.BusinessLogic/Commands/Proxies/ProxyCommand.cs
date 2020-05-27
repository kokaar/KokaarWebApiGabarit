﻿using AutoMapper;
using Catsa.Domain.Entities;
using Catsa.BusinessLogic.Exceptions;
using System;
using System.Text;
using Catsa.BusinessLogic.Enums;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.DataAccess.Repositories.Contracts;
using Catsa.BusinessLogic.Queries.Proxies;

namespace Catsa.BusinessLogic.Commands.Proxies
{
    public class ProxyCommand : BaseCommand<ProxyCommandDto, Proxy, int>, IProxyCommand
    {
        
        public ProxyCommand(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) {}

        protected override StringBuilder ValidateAdd(ProxyCommandDto proxyCommandDto)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (!proxyCommandDto.IsNew())
            {
                validationErrors.Append("La ressource que vous souhaitez ajouter existe déjà.");
                return validationErrors;
            }
            var validationResult = new ProxyValidator().Validate(proxyCommandDto);
            validationErrors.Append(validationResult.ToString());

            return validationErrors;
        }

        public override void Add(ProxyCommandDto proxyCommandDto)
        {
            var proxy = BuildEntity(proxyCommandDto);
            _unitOfWork.Proxy.Add(proxy);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateUpdate(ProxyCommandDto proxyCommandDto)
        {
            StringBuilder validationErrors = new StringBuilder();

            if (proxyCommandDto.IsNew())
            {
                validationErrors.Append("La ressource que vous souhaitez mettre à jour n'existe pas.");
                return validationErrors;
            }
            var validationResult = new ProxyValidator().Validate(proxyCommandDto);
            validationErrors.Append(validationResult.ToString());

            return validationErrors;
        }

        public override void Update(ProxyCommandDto proxyCommandDto)
        {
            var proxy = BuildEntity(proxyCommandDto);
            _unitOfWork.Proxy.Update(proxy);
            _unitOfWork.Save();
        }

        protected override StringBuilder ValidateDelete(ProxyCommandDto proxyCommandDto = null)
        {
            StringBuilder validationErrors = new StringBuilder();
            if (DataBaseAction != DataBaseActionEnum.Delete)
            {
                validationErrors.Append("DataBaseAction n'est pas mis à Delete.");
                return validationErrors;
            }

            return validationErrors;
        }

        public override void Delete(int proxyId)
        {
            var validationErrors = ValidateDelete();
            if (validationErrors.Length == 0)
            {
                _unitOfWork.Proxy.Delete(proxyId);
                _unitOfWork.Save();
            }
            else
            {
                throw new CommandValidationException(validationErrors.ToString());
            }
        }

    }
}
